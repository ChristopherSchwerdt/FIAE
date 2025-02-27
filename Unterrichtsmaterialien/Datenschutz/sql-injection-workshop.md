# SQL-Injektion Workshop

In diesem Workshop lernt ihr:
- Was SQL-Injektionen sind und wie sie funktionieren
- Wie man eine Webanwendung erstellt, die anfällig für SQL-Injektionen ist
- Wie man SQL-Injektionen durchführt
- Wie man Webanwendungen gegen SQL-Injektionen absichert

## Voraussetzungen

- Node.js (mindestens v14) installiert
- Grundkenntnisse in JavaScript, Express und SQL
- Ein Code-Editor eurer Wahl (VS Code empfohlen)

## 1. Projekt-Setup

### 1.1 Projekt erstellen

1. Öffnet ein Terminal und erstellt ein neues Projektverzeichnis:
   ```bash
   mkdir sql-injection-workshop
   cd sql-injection-workshop
   npm init -y
   ```

2. Installiert die benötigten Pakete:
   ```bash
   npm install express ejs prisma sqlite3 nodemon
   npm install -D @prisma/client
   ```
   (Anmerkung an mich: npm install sqlite3 `NICHT` über CWL-Firewall möglich!!!)

3. Initialisiert Prisma:
   ```bash
   npx prisma init --datasource-provider sqlite
   ```

### 1.2 Datenbankschema erstellen

1. Öffnet die Datei `prisma/schema.prisma` und ersetzt den Inhalt durch:
   ```prisma
   // prisma/schema.prisma
   datasource db {
     provider = "sqlite"
     url      = "file:./dev.db"
   }

   generator client {
     provider = "prisma-client-js"
   }

   model User {
     id       Int     @id @default(autoincrement())
     username String  @unique
     password String
     isAdmin  Boolean @default(false)
   }
   ```

2. Erstellt einen Ordner `prisma` (falls noch nicht vorhanden) und darin eine Datei `seed.js`:
   ```javascript
   // prisma/seed.js
   const { PrismaClient } = require('@prisma/client');
   const prisma = new PrismaClient();

   async function main() {
     await prisma.user.createMany({
       data: [
         { username: 'admin', password: 'admin123', isAdmin: true },
         { username: 'user1', password: 'password1', isAdmin: false },
         { username: 'user2', password: 'password2', isAdmin: false },
       ],
     });
   }

   main()
     .catch((e) => {
       console.error(e);
       process.exit(1);
     })
     .finally(async () => {
       await prisma.$disconnect();
     });
   ```

3. Öffnet `package.json` und fügt folgende Skripte hinzu:
   ```json
   {
     "scripts": {
       "start": "nodemon app.js",
       "seed": "node prisma/seed.js"
     },
     "prisma": {
       "seed": "node prisma/seed.js"
     }
   }
   ```

4. Erstellt die Datenbank und führt die Migration durch:
   ```bash
   npx prisma migrate dev --name init
   ```

5. Befüllt die Datenbank mit den Beispieldaten:
   ```bash
   npx prisma db seed
   ```

## 2. Anfällige Webanwendung erstellen

### 2.1 Express-Anwendung erstellen

1. Erstellt eine Datei `app.js` im Hauptverzeichnis:
   ```javascript
   // app.js
   const express = require('express');
   const { PrismaClient } = require('@prisma/client');
   const path = require('path');

   const app = express();
   const prisma = new PrismaClient();
   const PORT = 3000;

   // Middleware einrichten
   app.use(express.urlencoded({ extended: true }));
   app.set('view engine', 'ejs');
   app.set('views', path.join(__dirname, 'views'));

   // Startseite (Login-Formular)
   app.get('/', (req, res) => {
     res.render('login', { error: null });
   });

   // UNSICHERE Login-Route (anfällig für SQL-Injektion)
   app.post('/login', async (req, res) => {
     const { username, password } = req.body;
     
     // UNSICHERER CODE: Direkte Verkettung von Benutzereingaben in einer SQL-Abfrage
     const query = `SELECT * FROM User WHERE username = '${username}' AND password = '${password}'`;
     //Auskommentieren zum loggen:
     //console.log(query);
     
     try {
       // Führe die SQL-Abfrage direkt aus (UNSICHER!)
       const users = await prisma.$queryRawUnsafe(query);
       
       if (users && users.length > 0) {
         // Erfolgreiche Anmeldung
         res.render('dashboard', { user: users[0] });
       } else {
         // Fehlgeschlagene Anmeldung
         res.render('login', { error: 'Ungültiger Benutzername oder Passwort' });
       }
     } catch (error) {
       console.error('Login-Fehler:', error);
       res.render('login', { error: 'Ein Fehler ist aufgetreten' });
     }
   });

   // Admin-Bereich (geschützt)
   app.get('/admin', async (req, res) => {
     try {
       const users = await prisma.user.findMany();
       res.render('admin', { users });
     } catch (error) {
       res.status(500).send('Fehler beim Laden der Benutzer');
     }
   });

   // Server starten
   app.listen(PORT, () => {
     console.log(`Server läuft auf http://localhost:${PORT}`);
   });
   ```

### 2.2 EJS-Templates erstellen

1. Erstellt einen Ordner `views` im Hauptverzeichnis

2. Erstellt die Datei `views/login.ejs`:
   ```html
   <!DOCTYPE html>
   <html lang="de">
   <head>
     <meta charset="UTF-8">
     <meta name="viewport" content="width=device-width, initial-scale=1.0">
     <title>Login</title>
     <style>
       body { font-family: Arial, sans-serif; max-width: 500px; margin: 0 auto; padding: 20px; }
       .form-group { margin-bottom: 15px; }
       label { display: block; margin-bottom: 5px; }
       input[type="text"], input[type="password"] { width: 100%; padding: 8px; }
       button { padding: 10px 15px; background: #4CAF50; color: white; border: none; cursor: pointer; }
       .error { color: red; margin-bottom: 15px; }
     </style>
   </head>
   <body>
     <h1>Login</h1>
     
     <% if (error) { %>
       <div class="error"><%= error %></div>
     <% } %>
     
     <form action="/login" method="POST">
       <div class="form-group">
         <label for="username">Benutzername:</label>
         <input type="text" id="username" name="username" required>
       </div>
       
       <div class="form-group">
         <label for="password">Passwort:</label>
         <input type="password" id="password" name="password" required>
       </div>
       
       <button type="submit">Anmelden</button>
     </form>
   </body>
   </html>
   ```

3. Erstellt die Datei `views/dashboard.ejs`:
   ```html
   <!DOCTYPE html>
   <html lang="de">
   <head>
     <meta charset="UTF-8">
     <meta name="viewport" content="width=device-width, initial-scale=1.0">
     <title>Dashboard</title>
     <style>
       body { font-family: Arial, sans-serif; max-width: 800px; margin: 0 auto; padding: 20px; }
       .user-info { background: #f5f5f5; padding: 15px; border-radius: 5px; margin-bottom: 20px; }
       .admin-link { margin-top: 20px; }
       .admin-link a { padding: 10px 15px; background: #2196F3; color: white; text-decoration: none; border-radius: 3px; }
     </style>
   </head>
   <body>
     <h1>Dashboard</h1>
     
     <div class="user-info">
       <h2>Willkommen, <%= user.username %>!</h2>
       <p>Benutzer-ID: <%= user.id %></p>
       <% if (user.isAdmin) { %>
         <p><strong>Status: Administrator</strong></p>
       <% } else { %>
         <p>Status: Normaler Benutzer</p>
       <% } %>
     </div>
     
     <% if (user.isAdmin) { %>
       <div class="admin-link">
         <a href="/admin">Zum Admin-Bereich</a>
       </div>
     <% } %>
     
     <p><a href="/">Abmelden</a></p>
   </body>
   </html>
   ```

4. Erstellt die Datei `views/admin.ejs`:
   ```html
   <!DOCTYPE html>
   <html lang="de">
   <head>
     <meta charset="UTF-8">
     <meta name="viewport" content="width=device-width, initial-scale=1.0">
     <title>Admin-Bereich</title>
     <style>
       body { font-family: Arial, sans-serif; max-width: 800px; margin: 0 auto; padding: 20px; }
       table { width: 100%; border-collapse: collapse; }
       th, td { border: 1px solid #ddd; padding: 8px; text-align: left; }
       th { background-color: #f2f2f2; }
       tr:nth-child(even) { background-color: #f9f9f9; }
     </style>
   </head>
   <body>
     <h1>Admin-Bereich</h1>
     
     <h2>Alle Benutzer</h2>
     <table>
       <thead>
         <tr>
           <th>ID</th>
           <th>Benutzername</th>
           <th>Passwort</th>
           <th>Admin</th>
         </tr>
       </thead>
       <tbody>
         <% users.forEach(user => { %>
           <tr>
             <td><%= user.id %></td>
             <td><%= user.username %></td>
             <td><%= user.password %></td>
             <td><%= user.isAdmin ? 'Ja' : 'Nein' %></td>
           </tr>
         <% }); %>
       </tbody>
     </table>
     
     <p><a href="/">Zurück zum Login</a></p>
   </body>
   </html>
   ```

### 2.3 Anwendung starten

1. Startet die Anwendung:
   ```bash
   npm start
   ```

2. Öffnet euren Browser und navigiert zu: `http://localhost:3000`

3. Testet die Anmeldung mit den vordefinierten Benutzern:
   - Benutzername: `admin`, Passwort: `admin123`
   - Benutzername: `user1`, Passwort: `password1`

## 3. SQL-Injektionsangriffe durchführen

Jetzt werdet ihr verschiedene SQL-Injektionsangriffe gegen eure eigene Anwendung durchführen.

### 3.1 Einfacher Bypass-Angriff

1. Navigiert zur Login-Seite: `http://localhost:3000`
2. Gebt folgende Anmeldedaten ein:
   - Benutzername: `admin' --`
   - Passwort: (egal, kann leer bleiben)
3. Klickt auf "Anmelden"

**Erwartetes Ergebnis:** Ihr solltet als `admin` angemeldet sein, obwohl ihr das richtige Passwort nicht kennt.

**Erklärung:** 
Der eingegebene Benutzername wird zu folgendem SQL-Befehl:
```sql
SELECT * FROM User WHERE username = 'admin' --' AND password = ''
```
Der Teil `--` kommentiert den Rest der Abfrage aus, sodass die Passwortprüfung umgangen wird.

### 3.2 Anmeldung ohne gültigen Benutzernamen

1. Navigiert zur Login-Seite
2. Gebt folgende Anmeldedaten ein:
   - Benutzername: `' OR 1=1 --`
   - Passwort: (egal)
3. Klickt auf "Anmelden"

**Erwartetes Ergebnis:** Ihr solltet angemeldet sein, obwohl ihr keinen gültigen Benutzernamen eingegeben habt.

**Erklärung:**
Der eingegebene Benutzername wird zu folgendem SQL-Befehl:
```sql
SELECT * FROM User WHERE username = '' OR 1=1 --' AND password = ''
```
Die Bedingung `OR 1=1` ist immer wahr, sodass alle Benutzer zurückgegeben werden. Die Anwendung verwendet den ersten zurückgegebenen Benutzer (normalerweise den Admin).

### 3.3 Auflisten aller Benutzer

1. Navigiert zur Login-Seite
2. Gebt folgende Anmeldedaten ein:
   - Benutzername: `' UNION SELECT * FROM User --`
   - Passwort: (egal)
3. Klickt auf "Anmelden"

**Erwartetes Ergebnis:** Je nach SQL-Ausführung könntet ihr als einer der Benutzer angemeldet sein.

**Erklärung:**
Die UNION-Anweisung kombiniert die Ergebnisse der beiden SELECT-Anweisungen.

### 3.4 Dokumentation

Dokumentiert eure Angriffe:
1. Welche Angriffe waren erfolgreich?
2. Welche Daten konntet ihr aus der Datenbank extrahieren?
3. Welche weiteren Schäden könnten durch SQL-Injektionen verursacht werden?

## 4. Sicherheitsmaßnahmen implementieren

Jetzt werdet ihr eure Anwendung gegen SQL-Injektionen absichern.

### 4.1 Prisma ORM verwenden

1. Fügt eine neue sichere Login-Route in `app.js` hinzu:
   ```javascript
   // SICHERE Login-Route (mit Prisma ORM)
   app.post('/login-secure', async (req, res) => {
     const { username, password } = req.body;
     
     try {
       // SICHERER CODE: Verwendung von Prisma's ORM-Funktionen
       const user = await prisma.user.findFirst({
         where: {
           username: username,
           password: password
         }
       });
       
       if (user) {
         // Erfolgreiche Anmeldung
         res.render('dashboard', { user });
       } else {
         // Fehlgeschlagene Anmeldung
         res.render('login', { error: 'Ungültiger Benutzername oder Passwort' });
       }
     } catch (error) {
       console.error('Login-Fehler:', error);
       res.render('login', { error: 'Ein Fehler ist aufgetreten' });
     }
   });
   ```

2. Erstellt ein neues Login-Formular `views/login-secure.ejs` (kopiert das vorhandene und ändert den action-Pfad):
   ```html
   <form action="/login-secure" method="POST">
     <!-- Rest des Formulars bleibt gleich -->
   </form>
   ```

3. Fügt eine Route für das sichere Login-Formular in `app.js` hinzu:
   ```javascript
   app.get('/secure', (req, res) => {
     res.render('login-secure', { error: null });
   });
   ```

### 4.2 Prepared Statements verwenden

1. Fügt eine weitere sichere Login-Route in `app.js` hinzu:
   ```javascript
   // SICHERE Login-Route (mit Prepared Statements)
   app.post('/login-prepared', async (req, res) => {
     const { username, password } = req.body;
     
     try {
       // SICHERER CODE: Verwendung von parametrisierten Abfragen
       const users = await prisma.$queryRaw`
         SELECT * FROM User 
         WHERE username = ${username} 
         AND password = ${password}
       `;
       
       if (users && users.length > 0) {
         res.render('dashboard', { user: users[0] });
       } else {
         res.render('login', { error: 'Ungültiger Benutzername oder Passwort' });
       }
     } catch (error) {
       console.error('Login-Fehler:', error);
       res.render('login', { error: 'Ein Fehler ist aufgetreten' });
     }
   });
   ```

2. Erstellt ein neues Login-Formular `views/login-prepared.ejs`:
   ```html
   <form action="/login-prepared" method="POST">
     <!-- Rest des Formulars bleibt gleich -->
   </form>
   ```

3. Fügt eine Route für dieses Login-Formular in `app.js` hinzu:
   ```javascript
   app.get('/prepared', (req, res) => {
     res.render('login-prepared', { error: null });
   });
   ```

### 4.3 Input-Validierung implementieren

1. Fügt eine weitere Login-Route mit Validierung in `app.js` hinzu:
   ```javascript
   // Login-Route mit Input-Validierung
   app.post('/login-validation', async (req, res) => {
     const { username, password } = req.body;
     
     // Einfache Validierung
     if (!username || !password || username.includes("'") || password.includes("'")) {
       return res.render('login', { error: 'Ungültige Eingabe' });
     }
     
     try {
       const user = await prisma.user.findFirst({
         where: {
           username,
           password
         }
       });
       
       if (user) {
         res.render('dashboard', { user });
       } else {
         res.render('login', { error: 'Ungültiger Benutzername oder Passwort' });
       }
     } catch (error) {
       console.error('Login-Fehler:', error);
       res.render('login', { error: 'Ein Fehler ist aufgetreten' });
     }
   });
   ```

2. Erstellt ein neues Login-Formular `views/login-validation.ejs`:
   ```html
   <form action="/login-validation" method="POST">
     <!-- Rest des Formulars bleibt gleich -->
   </form>
   ```

3. Fügt eine Route für dieses Login-Formular in `app.js` hinzu:
   ```javascript
   app.get('/validation', (req, res) => {
     res.render('login-validation', { error: null });
   });
   ```

### 4.4 Testen der sicheren Implementierungen

1. Testet alle SQL-Injektionsangriffe gegen die sicheren Implementierungen
  - Secure: `http://localhost:3000/secure`
  - Prepared: `http://localhost:3000/prepared`
  - Validation: `http://localhost:3000/validation`

3. Dokumentiert die Ergebnisse:
   - Welche Angriffe funktionieren nicht mehr?
   - Welche Sicherheitsmethode ist am einfachsten zu implementieren?
   - Welche Methode bietet den besten Schutz?

## 5. Übung: Vollständig sichere Anwendung

Implementiert nun eure eigene vollständig sichere Anwendung:

1. Verwendet eine Kombination aus:
   - ORM (Prisma)
   - Parametrisierte Abfragen
   - Input-Validierung
   - Minimalen Zugriffsrechten

2. Erstellt ein Login-Formular, das alle Sicherheitsmaßnahmen implementiert

3. Testet die Anwendung mit verschiedenen SQL-Injektionsangriffen

4. Dokumentiert eure Ergebnisse:
   - Welche Schwachstellen habt ihr gefunden und behoben?
   - Welche zusätzlichen Sicherheitsmaßnahmen könntet ihr implementieren?
   - Wie würdet ihr die Anwendung in einer Produktionsumgebung absichern?

## 6. Weiterführende Aufgaben

Wenn ihr frühzeitig fertig seid, probiert diese zusätzlichen Aufgaben aus:

1. **Passwort-Hashing implementieren:**
   - Installiert bcrypt: `npm install bcrypt`
   - Fügt Hashing bei der Benutzerregistrierung hinzu
   - Vergleicht Passwörter sicher bei der Anmeldung

2. **JSON Web Token (JWT) Authentication:**
   - Installiert jsonwebtoken: `npm install jsonwebtoken`
   - Implementiert eine Token-basierte Authentifizierung
   - Schützt Routen mit einem Auth-Middleware

3. **Weitere anfällige Funktionen:**
   - Erstellt eine Suchfunktion, die anfällig für SQL-Injektionen ist
   - Sichert diese anschließend ab


