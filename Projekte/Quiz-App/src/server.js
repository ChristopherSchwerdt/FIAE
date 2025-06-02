const express = require('express');
const http = require('http');
const { Server } = require('socket.io');
const { PrismaClient } = require('@prisma/client');
const dotenv = require('dotenv');
const cors = require('cors');

dotenv.config();
const prisma = new PrismaClient();
const app = express();
const server = http.createServer(app);
const io = new Server(server);
const os = require('os');

app.use(cors());
app.use(express.json());
app.use(express.static('src/public')); // Stelle sicher, dass dein Adminpanel hier liegt!

//Der Port über den das Quiz erreichbar sein soll:
const PORT = process.env.PORT || 3000;

// Session-Verwaltung
let sessions = {};

// API: Kategorien abrufen
app.get('/api/categories', async (req, res) => {
  const categories = await prisma.question.findMany({
    distinct: ['category'],
    select: { category: true }
  });
  res.json(categories.map(c => c.category));
});

//API: Lokale Adresse des Host abbrufen:
app.get('/api/local-ip', (req, res) => {
  const interfaces = os.networkInterfaces();
  const ips = Object.values(interfaces)
    .flat()
    .filter(i => i.family === 'IPv4' && !i.internal)
    .map(i => i.address);
  res.json({ ip: ips[0] || 'unbekannt' });
});

//API: Verbundene Clients zählen und abfragen:
app.get('/api/client-count/:code', (req, res) => {
  const code = req.params.code;
  const room = io.sockets.adapter.rooms.get(code);
  const count = room ? room.size - 1 : 0; // -1 für das Adminpanel/ DEn Host selbst
  res.json({ count: Math.max(0, count) });
});

// API: Quizcode generieren + Session anlegen
app.post('/api/generate-code', async (req, res) => {
  const code = Math.floor(100000 + Math.random() * 900000).toString();
  const questions = await prisma.question.findMany({
    where: { category: req.body.category }
  });

  sessions[code] = {
    current: 0,
    category: req.body.category,
    questions,
    answers: {}
  };
  console.log(`[Quiz-Start] Neues Quiz mit dem Code ${code} wurde erstellt!`);
  res.json({ code, total: questions.length });
});

//API: Frage erstellen(Datamanager)
app.post('/api/questions', async (req, res) => {
  try {
    const data = req.body;
    const newQuestion = await prisma.question.create({ data });
    res.status(201).json(newQuestion);
  } catch (error) {
    res.status(500).json({ error: 'Fehler beim Erstellen der Frage.' });
  }
});
//API: Frage aktualisieren(Datamanager)
app.put('/api/questions/:id', async (req, res) => {
  try {
    const id = parseInt(req.params.id);
    const data = req.body;

    const updated = await prisma.question.update({
      where: { id },
      data,
    });

    res.json(updated);
  } catch (error) {
    res.status(500).json({ error: 'Fehler beim Aktualisieren der Frage.' });
  }
});

//API: Alle Fragen abrufen(Datamanager)
app.get('/api/questions', async (req, res) => {
  try {
    const questions = await prisma.question.findMany();
    res.json(questions);
  } catch (error) {
    res.status(500).json({ error: 'Fehler beim Laden der Fragen.' });
  }
});

//API: Frage löschen(Datamanager)
app.delete('/api/questions/:id', async (req, res) => {
  try {
    const id = parseInt(req.params.id);
    await prisma.question.delete({ where: { id } });
    res.status(204).end();
  } catch (error) {
    res.status(500).json({ error: 'Fehler beim Löschen der Frage.' });
  }
});

// WebSocket-Handling
io.on('connection', (socket) => {
   socket.on('start-question', ({ code }) => {
    const session = sessions[code];
    if (session && session.current < session.questions.length) {
      const question = session.questions[session.current];

      session.answers[question.id] = {}; // Antworten zurücksetzen

      io.to(code).emit('show-question', {
        id: question.id,
        text: question.text,
        optionA: question.optionA,
        optionB: question.optionB,
        optionC: question.optionC,
        optionD: question.optionD,
        category: session.category
  });
  }
  });
  //Websocket: Nächste Frage initieren:
  socket.on('next-question', ({ code }) => {
  const session = sessions[code];
  if (!session) return;

  if (session.current < session.questions.length) {
    const question = session.questions[session.current];

    // Antworten für diese Frage zurücksetzen
    session.answers[question.id] = {};
   
    // An alle im Raum senden
    io.to(code).emit('show-question', {
      id: question.id,
      text: question.text,
      optionA: question.optionA,
      optionB: question.optionB,
      optionC: question.optionC,
      optionD: question.optionD,
      category: session.category
    });
    console.log(`[Frage gesendet] Neue Frage für Code ${code} wurde gesendet!`);
     session.current++; // jetzt zur nächsten Frage vorgehen
     
  } else {
    io.to(code).emit('quiz-complete'); // optionales Event für Ende
    console.log(`[Quiz-Ende] Keine weiteren Fragen für Code ${code}`);
  }
});
//Websocket: Einer Quiz-Session joinen:
  socket.on('join-quiz', ({ code }) => {
  if (!sessions[code]) {
    socket.emit('error-message', 'Ungültiger Code oder Session nicht gefunden.');
    return;
  }
  socket.join(code);
  const count = io.sockets.adapter.rooms.get(code)?.size || 0;
  io.to(code).emit('joined-count', count);
  console.log(`[Client joined] neuer Client für Code ${code}`);
});
  socket.on('disconnect', () => {    
    console.log(`[Client disconnected] Client hat verbindung getrennt!`); 
    for (const [room, sockets] of io.sockets.adapter.rooms.entries()) {
      const count = sockets.size;
      io.to(room).emit('joined-count', count); 
      
    }
  });

  
//Websocket: Eine Antwort übertragen:
  socket.on('answer', ({ code, questionId, option }) => {
  console.log(`[Client Antwort] code: ${code}, frage: ${questionId}, gewählt: ${option}`);
  if (!sessions[code]) return;

  if (!sessions[code].answers[questionId]) {
    sessions[code].answers[questionId] = {};
  }

  sessions[code].answers[questionId][socket.id] = option;

  // sende an Adminpanel (und ggf. Clients)
  io.to(code).emit('answer-stat', { questionId, option });
});
//Websocket: Die richtige Lösung anfragen:
  socket.on('show-results', ({ code }) => {
    
    const session = sessions[code];
    if (!session) return;

    const question = session.questions[session.current-1];
    const answers = session.answers[question.id] || {};
    const results = { A: 0, B: 0, C: 0, D: 0 };
     //console.log(`[question id]: ${question.id}, Session id: ${session.current}`);

    Object.values(answers).forEach(opt => {
      if (results[opt] !== undefined) results[opt]++;
    });

    io.to(code).emit('show-results', {
      results,
      correct: question.correct,
      question: question.text
    });
    console.log(`[Ergebnisse] Zeige Ergebnisse für aktuelle Frage Code ${code}`);
    //session.current++;
  });
});

server.listen(PORT, () => {
  console.log(`Quiz Server läuft auf http://localhost:${PORT}`);
  //console.log(`Adminpanel läuft auf http://localhost:${PORT}/adminpanel.html`);
});
