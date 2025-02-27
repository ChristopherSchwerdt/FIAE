# Workshop 1: Secure Coding

## Injection Prevention: Einführung und Grundlagenwebseite erstellen

### Phase 1: Aufbau der Basis-Webanwendung 

**Schritt-für-Schritt-Anleitung:**

1. **Projekt einrichten:**
   - Erstelle im htdocs-Verzeichnis des XAMPP-Servers einen neuen Ordner "SecurityApp"
   - Starte XAMPP und aktiviere Apache und MySQL

2. **Datenbank erstellen:**
   - Öffne phpMyAdmin im Browser (http://localhost/phpmyadmin)
   - Erstelle eine neue Datenbank "security_db"
   - Führe folgenden SQL-Code aus, um eine Benutzertabelle zu erstellen:

```sql
CREATE TABLE users (
    id INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(50) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    email VARCHAR(100),
    is_admin BOOLEAN DEFAULT FALSE
);

INSERT INTO users (username, password, email, is_admin) 
VALUES 
('admin', 'admin123', 'admin@example.com', TRUE),
('user1', 'pass1234', 'user1@example.com', FALSE),
('user2', 'qwerty', 'user2@example.com', FALSE);
```

3. **Datenbankverbindung einrichten:**
   - Erstelle eine Datei `db_connect.php` mit folgendem Inhalt:

```php
<?php
$host = 'localhost';
$dbname = 'security_db';
$username = 'root';
$password = '';

try {
    $db = new PDO("mysql:host=$host;dbname=$dbname", $username, $password);
    $db->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
} catch(PDOException $e) {
    echo "Verbindungsfehler: " . $e->getMessage();
    die();
}
?>
```

4. **Login-Formular erstellen:**
   - Erstelle eine Datei `index.php` mit folgendem Inhalt:

```php
<?php
session_start();
include 'db_connect.php';

// Unsichere Login-Funktion (anfällig für SQL-Injection)
if ($_SERVER["REQUEST_METHOD"] == "POST") {
    $username = $_POST['username'];
    $password = $_POST['password'];
    
    $query = "SELECT * FROM users WHERE username = '$username' AND password = '$password'";
    $result = $db->query($query);
    
    if ($result->rowCount() > 0) {
        $user = $result->fetch(PDO::FETCH_ASSOC);
        $_SESSION['user_id'] = $user['id'];
        $_SESSION['username'] = $user['username'];
        $_SESSION['is_admin'] = $user['is_admin'];
        header("Location: dashboard.php");
        exit;
    } else {
        $error = "Ungültige Anmeldeinformationen";
    }
}
?>

<!DOCTYPE html>
<html>
<head>
    <title>Sicherheitsübung - Login</title>
    <style>
        body { font-family: Arial, sans-serif; margin: 40px; }
        .container { max-width: 400px; margin: 0 auto; padding: 20px; border: 1px solid #ddd; }
        .error { color: red; margin-bottom: 15px; }
        input[type=text], input[type=password] { width: 100%; padding: 10px; margin: 8px 0; box-sizing: border-box; }
        button { background-color: #4CAF50; color: white; padding: 10px 15px; border: none; cursor: pointer; }
    </style>
</head>
<body>
    <div class="container">
        <h2>Login</h2>
        <?php if (isset($error)) { echo "<div class='error'>$error</div>"; } ?>
        <form method="post" action="">
            <div>
                <label for="username">Benutzername:</label>
                <input type="text" id="username" name="username" required>
            </div>
            <div>
                <label for="password">Passwort:</label>
                <input type="password" id="password" name="password" required>
            </div>
            <button type="submit">Anmelden</button>
        </form>
    </div>
</body>
</html>
```

5. **Dashboard erstellen:**
   - Erstelle eine Datei `dashboard.php` mit folgendem Inhalt:

```php
<?php
session_start();
include 'db_connect.php';

// Überprüfe, ob der Benutzer angemeldet ist
if (!isset($_SESSION['user_id'])) {
    header("Location: index.php");
    exit;
}

// Benutzerinformationen anzeigen
$username = $_SESSION['username'];
$is_admin = $_SESSION['is_admin'] ? 'Ja' : 'Nein';

// Unsichere Suchfunktion (anfällig für SQL-Injection)
$searchResults = [];
if (isset($_GET['search'])) {
    $search = $_GET['search'];
    $query = "SELECT * FROM users WHERE username LIKE '%$search%' OR email LIKE '%$search%'";
    $result = $db->query($query);
    $searchResults = $result->fetchAll(PDO::FETCH_ASSOC);
}
?>

<!DOCTYPE html>
<html>
<head>
    <title>Dashboard</title>
    <style>
        body { font-family: Arial, sans-serif; margin: 40px; }
        .container { max-width: 800px; margin: 0 auto; }
        .user-info { background: #f9f9f9; padding: 15px; margin-bottom: 20px; }
        .search { margin-bottom: 20px; }
        table { width: 100%; border-collapse: collapse; }
        th, td { border: 1px solid #ddd; padding: 8px; text-align: left; }
        th { background-color: #f2f2f2; }
        .logout { text-align: right; }
    </style>
</head>
<body>
    <div class="container">
        <div class="logout">
            <a href="logout.php">Abmelden</a>
        </div>
        <h2>Dashboard</h2>
        <div class="user-info">
            <p><strong>Benutzername:</strong> <?php echo $username; ?></p>
            <p><strong>Admin:</strong> <?php echo $is_admin; ?></p>
        </div>
        
        <div class="search">
            <h3>Benutzersuche</h3>
            <form method="get">
                <input type="text" name="search" placeholder="Suche nach Benutzername oder E-Mail">
                <button type="submit">Suchen</button>
            </form>
        </div>
        
        <?php if (!empty($searchResults)): ?>
            <h3>Suchergebnisse</h3>
            <table>
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Benutzername</th>
                        <th>E-Mail</th>
                        <th>Admin</th>
                    </tr>
                </thead>
                <tbody>
                    <?php foreach ($searchResults as $user): ?>
                        <tr>
                            <td><?php echo $user['id']; ?></td>
                            <td><?php echo $user['username']; ?></td>
                            <td><?php echo $user['email']; ?></td>
                            <td><?php echo $user['is_admin'] ? 'Ja' : 'Nein'; ?></td>
                        </tr>
                    <?php endforeach; ?>
                </tbody>
            </table>
        <?php endif; ?>
    </div>
</body>
</html>
```

6. **Logout-Funktion erstellen:**
   - Erstelle eine Datei `logout.php` mit folgendem Inhalt:

```php
<?php
session_start();
session_destroy();
header("Location: index.php");
exit;
?>
```
7. **Testen der Anwendung **

- teste deine Anwendung mit den vorgegebenen Zugangsdaten
- Überprüfe, ob die Anmeldung funktioniert und das Dashboard angezeigt wird
