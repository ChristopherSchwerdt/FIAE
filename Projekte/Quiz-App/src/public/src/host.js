    let currentCode = "";
    let currentQuestion = null;
    let category = "";
    let currentQuestionNumber = 0;
    let totalQuestionCount = 0;
    const socket = io();
    const voteCounts = { A: 0, B: 0, C: 0, D: 0 };
    let showVotes = false;
    let totalAnswers = 0;

    async function loadCategories() {
      const res = await fetch('/api/categories');
      const data = await res.json();
      const select = document.getElementById('category');
      data.forEach(cat => {
        const option = document.createElement('option');
        option.value = cat;
        option.textContent = cat;
        select.appendChild(option);
      });
    }

    async function startQuiz() {
      const startBtn = document.getElementById('start-btn');

      if (!currentCode) {
        category = document.getElementById('category').value;
        const res = await fetch('/api/generate-code', {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify({ category })
        });
        const data = await res.json();
        currentCode = data.code;
        totalQuestionCount = data.total || 0; // falls verfügbar vom Server
        document.getElementById('code-display').textContent = currentCode;
        document.getElementById('total-questions').textContent = totalQuestionCount;
        document.getElementById('current-question').textContent = currentQuestionNumber;

        socket.emit('join-quiz', { code: currentCode });

        setInterval(() => {
          if (currentCode) {
            fetch(`/api/client-count/${currentCode}`)
              .then(res => res.json())
              .then(data => {
                document.getElementById('client-count').textContent = data.count;
              });
          }
        }, 5000);
      }
      else{
        //socket.emit('start-question', { code: currentCode });
        showNextQuestion();
        startBtn.style.display='none';
        document.querySelector('.info-bar').style.display = 'flex';
        document.querySelector('.verbindungsbar').style.display = 'none';
      }
      

      startBtn.textContent = "Quiz starten";
      totalAnswers = 0;
      showVotes = false;
      document.getElementById('answer-count').textContent = "0";
    }

    function showResults() {
      socket.emit('show-results', { code: currentCode }); 
    }

    function revealVotes() {
      showVotes = true;
      updateResultsList();
    }

    function showNextQuestion() {
      if(currentQuestionNumber < totalQuestionCount)
    {
      socket.emit('next-question', { code: currentCode });
      currentQuestionNumber++;
      document.getElementById('current-question').textContent = currentQuestionNumber;
      totalAnswers = 0;
      showVotes = false;
      document.getElementById('answer-count').textContent = "0";
    }
      

    }

    socket.on('joined-count', count => {
      document.getElementById('client-count').textContent = count;
    });

    socket.on('show-question', data => {
      currentQuestion = data;
      voteCounts.A = 0; voteCounts.B = 0; voteCounts.C = 0; voteCounts.D = 0;
      totalAnswers = 0;
      showVotes = false;
      document.getElementById('question-block').style.display = 'block';
      document.getElementById('question-text').textContent = data.text;
      document.getElementById('answer-count').textContent = "0";
      updateResultsList();
    });

    socket.on('answer-stat', ({ questionId, option }) => {
      if (!voteCounts[option]) voteCounts[option] = 0;
      voteCounts[option]++;
      totalAnswers++;
      document.getElementById('answer-count').textContent = totalAnswers;
      updateResultsList();
    });

    socket.on('show-results', data => {
      highlightCorrectAnswer(data.correct);
    });

    function updateResultsList() {
      const list = document.getElementById('results-list');
      list.innerHTML = '';
      ['A', 'B', 'C', 'D'].forEach(letter => {
        const optionText = currentQuestion[`option${letter}`];
        if (optionText) {
          const li = document.createElement('li');
          li.id = `option-${letter}`;
          li.className = 'option-line';
          li.textContent = `${letter}: ${optionText}`;
          if (showVotes) li.textContent += ` (${voteCounts[letter]} Stimmen)`;
          list.appendChild(li);
        }
      });
    }

    function highlightCorrectAnswer(correct) {
      ['A', 'B', 'C', 'D'].forEach(letter => {
        const el = document.getElementById(`option-${letter}`);
        if (el) el.classList.remove('correct');
      });
      const correctEl = document.getElementById(`option-${correct}`);
      if (correctEl) correctEl.classList.add('correct');
    }
    // Lokale IP über Server abrufen (muss API bereitstellen!)
    async function fetchLocalIP() {
    try {
      const res = await fetch('/api/local-ip');
      const data = await res.json();
      document.getElementById('local-ip').textContent = data.ip;
    } catch (err) {
      document.getElementById('local-ip').textContent = 'Fehler beim Laden';
    }
    }
    fetchLocalIP();
    loadCategories();