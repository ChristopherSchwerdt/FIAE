using System.Data.SQLite;

namespace Buecherdatenbank
{
    class Datenbank
    {
        SQLiteConnection _connection;
        public void Open()
        {
            _connection = new SQLiteConnection(@"Data Source=C:\dev\Databases\Buecherdatenbank.db");
            _connection.Open();
            CreateDatabaseTablesIfNotExists();
            
        }
        public void Close()
        {
            _connection.Close();
        }
        private void CreateDatabaseTablesIfNotExists()
        {
            //Tabelle namens "Bücher" enthält. Die Tabelle "Bücher"
            //sollte die Spalten ISBN (int, Primärschlüssel),
            //Titel (varchar), Autor (varchar),
            //Verlag (varchar) und Erscheinungsjahr (int) enthalten.

            string SQL = @"
            CREATE TABLE IF NOT EXISTS Buecher(
            ISBN INTEGER PRIMARY KEY,
            Titel VARCHAR(50) NOT NULL,
            Autor VARCHAR(20) NOT NULL,
            Verlag Varchar(20),
            Erscheinungsjahr INTEGER);";

            SQLiteCommand createDatabaseCommand = new SQLiteCommand(SQL,_connection);
            createDatabaseCommand.ExecuteNonQuery();
        }
        public List<string> GetAllBooks()
        {
            List<string> books = new List<string>();
            
            string SQL = @"
            SELECT Titel
            FROM Buecher";

            SQLiteCommand selectCommand = new SQLiteCommand(SQL,_connection);
            SQLiteDataReader reader = selectCommand.ExecuteReader();
            while (reader.Read())
            {
                books.Add(reader.GetString(0));
            }

            return books;
        }
        //Gibt alle Bücher zurück dessen Titel den Begriff enthalten
        // title == "Potter"
        public List<Buch> GetBooksByTitle(string title)
        {
            List<Buch> books = new List<Buch>();

            string SQL = @"
            SELECT * FROM Buecher
            WHERE Titel LIKE $title";

            SQLiteCommand selectCommand = new SQLiteCommand(SQL, _connection);
            selectCommand.Parameters.AddWithValue("$title", "%"+title+"%");
            SQLiteDataReader reader = selectCommand.ExecuteReader();
            while (reader.Read())
            {
                // Antwort von der Datenbank
                Buch buch = new Buch();
                //ISBN
                buch.isbn = reader.GetInt64(0);
                //Titel
                buch.titel = reader.GetString(1);
                //Autor
                buch.autor = reader.GetString(2);
                //Verlag
                buch.verlag = reader.GetString(3);
                //Erscheinungsjahr
                buch.erscheinungsjahr = reader.GetInt32(4);

                books.Add(buch);
            }

            return books;
        }

        public List<Buch> GetAllBooksWithValues()
        {
            List<Buch> books = new List<Buch>();

            string SQL = @"
            SELECT *
            FROM Buecher";

            SQLiteCommand selectCommand = new SQLiteCommand(SQL, _connection);
            SQLiteDataReader reader = selectCommand.ExecuteReader();
            while (reader.Read())
            {
                Buch buch = new Buch();
                //ISBN
                buch.isbn = reader.GetInt64(0);
                //Titel
                buch.titel = reader.GetString(1);
                //Autor
                buch.autor = reader.GetString(2);
                //Verlag
                buch.verlag = reader.GetString(3);
                //Erscheinungsjahr
                buch.erscheinungsjahr = reader.GetInt32(4);
                books.Add(buch);
            }

            return books;
        }

        internal void AddBook(long iSBN, string? titel, string? autor, string? verlag, int erscheinungsjahr)
        {
            //INSERT SQl Befehl
            string SQL = @"
            INSERT INTO Buecher(ISBN,Titel,Autor,Verlag,Erscheinungsjahr)
            VALUES($1,$2,$3,$4,$5);";

            SQLiteCommand insertCommand = new SQLiteCommand( SQL,_connection);
            //Ersetze die Werte $1...$5 mit den übergebenen Variablen
            insertCommand.Parameters.AddWithValue("$1",iSBN);
            insertCommand.Parameters.AddWithValue("$2",titel);
            insertCommand.Parameters.AddWithValue("$3",autor);
            insertCommand.Parameters.AddWithValue("$4",verlag);
            insertCommand.Parameters.AddWithValue("$5",erscheinungsjahr);
            //Führe den Insert Befehl in der Datenbank aus
            insertCommand.ExecuteNonQuery();
        }
        public Buch SearchForISBN(long isbn)
        {
            //Ein neues Objekt vom Typ Buch erstellen
            Buch buch = new Buch();

            string SQL = @"
            SELECT * FROM Buecher
            WHERE ISBN = $isbn;";

            SQLiteCommand selectCommand = new SQLiteCommand(SQL,_connection);
            selectCommand.Parameters.AddWithValue("$isbn", isbn);
            SQLiteDataReader reader = selectCommand.ExecuteReader();
            while (reader.Read())
            {
                //ISBN
                buch.isbn= reader.GetInt64(0);
                //Titel
                buch.titel= reader.GetString(1);
                //Autor
                buch.autor = reader.GetString(2);
                //Verlag
                buch.verlag = reader.GetString(3);
                //Erscheinungsjahr
                buch.erscheinungsjahr=  reader.GetInt32(4);                               
            }
            return buch;
        }

        public void UpdateBook(Buch buch)
        {
            string SQL = @"
            UPDATE Buecher
            SET Titel = $1,
            Autor = $2,
            Verlag = $3,
            Erscheinungsjahr = $4
            WHERE ISBN = $isbn;";

            SQLiteCommand updateCommand = new SQLiteCommand(SQL,_connection);
            updateCommand.Parameters.AddWithValue("$1",buch.titel);
            updateCommand.Parameters.AddWithValue("$2", buch.autor);
            updateCommand.Parameters.AddWithValue("$3", buch.verlag);
            updateCommand.Parameters.AddWithValue("$4", buch.erscheinungsjahr);
            updateCommand.Parameters.AddWithValue("$isbn", buch.isbn);

            updateCommand.ExecuteNonQuery();
        }

        internal List<Buch> GetBooksByAuthor(string Author)
        {
            List<Buch> books = new List<Buch>();

            string SQL = @"
            SELECT * FROM Buecher
            WHERE Autor LIKE $autor";

            SQLiteCommand selectCommand = new SQLiteCommand(SQL, _connection);
            selectCommand.Parameters.AddWithValue("$autor", "%" + Author + "%");
            SQLiteDataReader reader = selectCommand.ExecuteReader();
            while (reader.Read())
            {
                // Antwort von der Datenbank
                Buch buch = new Buch();
                //ISBN
                buch.isbn = reader.GetInt64(0);
                //Titel
                buch.titel = reader.GetString(1);
                //Autor
                buch.autor = reader.GetString(2);
                //Verlag
                buch.verlag = reader.GetString(3);
                //Erscheinungsjahr
                buch.erscheinungsjahr = reader.GetInt32(4);

                books.Add(buch);
            }

            return books;
        }

        internal List<Buch> GetBooksByYear(int Year)
        {
            List<Buch> books = new List<Buch>();

            string SQL = @"
            SELECT * FROM Buecher
            WHERE Erscheinungsjahr = $jahr";

            SQLiteCommand selectCommand = new SQLiteCommand(SQL, _connection);
            selectCommand.Parameters.AddWithValue("$jahr",Year);
            SQLiteDataReader reader = selectCommand.ExecuteReader();
            while (reader.Read())
            {
                // Antwort von der Datenbank
                Buch buch = new Buch();
                //ISBN
                buch.isbn = reader.GetInt64(0);
                //Titel
                buch.titel = reader.GetString(1);
                //Autor
                buch.autor = reader.GetString(2);
                //Verlag
                buch.verlag = reader.GetString(3);
                //Erscheinungsjahr
                buch.erscheinungsjahr = reader.GetInt32(4);

                books.Add(buch);
            }

            return books;
        }
    }

}
