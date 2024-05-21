using System.Data;
using System.Data.SQLite;

namespace SQL_Lernen
{
    internal class Database
    {
        private SQLiteConnection _SQLiteConnection;
        private string DatabaseFile;
        public string LastError;
        public Database() {

            DatabaseFile = Environment.CurrentDirectory + "\\Datenbank.db";
                        
            if(!File.Exists(DatabaseFile))
            {
                _SQLiteConnection = new SQLiteConnection(@"Data Source=" + DatabaseFile);
                _SQLiteConnection.Open();
                GenerateDatabase();
            }
            else
            {
                _SQLiteConnection = new SQLiteConnection(@"Data Source=" + DatabaseFile);
                _SQLiteConnection.Open();
            }
            
        }
        public int ExecuteNonQuery(string sql)
        {
            try
            {
                SQLiteCommand executeCommand = new SQLiteCommand(sql, _SQLiteConnection);
                return executeCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return -1;
            }
            
        }
        public DataTable GetDatatable(string sql)
        {
            try
            {
                DataTable dt = new DataTable();

                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, _SQLiteConnection);
                using (SQLiteCommandBuilder command = new SQLiteCommandBuilder(adapter))
                {
                    // Populate datatable to return, using the database adapter                
                    adapter.Fill(dt);
                }
                return dt;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return null;
            }
            
        }
        public void ResetDatabase()
        {
            _SQLiteConnection.Close();
            File.Delete(DatabaseFile);
            _SQLiteConnection = new SQLiteConnection(@"Data Source=" + DatabaseFile);
            _SQLiteConnection.Open();
            GenerateDatabase();

        }
        public void GenerateDatabase() {

            string SQL = @"
            CREATE TABLE IF NOT EXISTS Fahrer(
            Personalnummer INTEGER PRIMARY KEY AUTOINCREMENT,
            Vorname VARCHAR(20) NOT NULL,
            Name VARCHAR(20) NOT NULL,
            StrasseNr VARCHAR(20) NOT NULL,
            Plz VARCHAR(5) NOT NULL,
            Ort VARCHAR(20) NOT NULL,
            Telefon VARCHAR(20) NOT NULL);";

            SQLiteCommand createDBCommand = new SQLiteCommand(_SQLiteConnection);
            createDBCommand.CommandText = SQL;
            createDBCommand.ExecuteNonQuery();

            string SQL2 = @"
            CREATE TABLE IF NOT EXISTS Bus(
            Kennzeichen VARCHAR(10) PRIMARY KEY,
            Baujahr INTEGER NOT NULL,
            AnzSitzplaetze INTEGER NOT NULL);";
            
            createDBCommand.CommandText = SQL2;
            createDBCommand.ExecuteNonQuery();

            string SQL3 = @"
            CREATE TABLE IF NOT EXISTS Fahrt(
            FahrtNr INTEGER PRIMARY KEY AUTOINCREMENT,
            Kennzeichen VARCHAR(10) NOT NULL,
            PersonalNr INTEGER NOT NULL,
            Datum DATE NOT NULL,
            Preis DECIMAL(19,4) NOT NULL,
            Dauer INTEGER NOT NULL,
            Reisestart VARCHAR(30) NOT NULL,
            Reiseziel VARCHAR(30) NOT NULL);";
            createDBCommand.CommandText = SQL3;
            createDBCommand.ExecuteNonQuery();

            string SQL4 = @"
            CREATE TABLE IF NOT EXISTS Kunde(
            KundeNr INTEGER PRIMARY KEY AUTOINCREMENT,
            Stammkunde BOOLEAN NOT NULL,
            Vorname VARCHAR(20) NOT NULL,
            Name VARCHAR(20) NOT NULL,
            Telefon VARCHAR(30) NOT NULL,
            StrasseNr VARCHAR(30) NOT NULL,
            Plz INTEGER NOT NULL,
            Ort VARCHAR(20) NOT NULL);";
            createDBCommand.CommandText = SQL4;
            createDBCommand.ExecuteNonQuery();

            string INSERTSQL = @"
            INSERT INTO Bus (Kennzeichen,Baujahr,AnzSitzplaetze)
            VALUES ('EL-BS 101',1988,60);";
            ExecuteNonQuery(INSERTSQL);

            string INSERTSQL2 = @"
            INSERT INTO Bus (Kennzeichen,Baujahr,AnzSitzplaetze)
            VALUES ('EL-BS 102',1987,65);";
            ExecuteNonQuery(INSERTSQL2);

            string INSERTSQL3 = @"
            INSERT INTO Bus (Kennzeichen,Baujahr,AnzSitzplaetze)
            VALUES ('EL-BS 103',1988,70);";
            ExecuteNonQuery(INSERTSQL3);

            string INSERTFahrer = @"
            INSERT INTO Fahrer(Vorname,Name,StrasseNr,Plz,Ort,Telefon)
VALUES ('Christopher','Schwerdt','Kirchweg 5',49744,'Geeste',0123456789);";
            ExecuteNonQuery(INSERTFahrer);
            string INSERTFahrer2 = @"
            INSERT INTO Fahrer(Vorname,Name,StrasseNr,Plz,Ort,Telefon)
VALUES ('Bernd','Buchner','Schwarzerweg 15',49809,'Lingen(Ems)',0123445789);";
            ExecuteNonQuery(INSERTFahrer2);
            string INSERTFahrer3 = @"
            INSERT INTO Fahrer(Vorname,Name,StrasseNr,Plz,Ort,Telefon)
VALUES ('Maria','Schmidt','Rathsweg 4',49809,'Lingen(Ems)',01234343489);";
            ExecuteNonQuery(INSERTFahrer3);

            string InsertKunde = @"
            INSERT INTO Kunde(Stammkunde,Vorname,Name,Telefon,StrasseNr,Plz,Ort)
VALUES (0,'Silvia','Kruse',0230233222,'Baumstraße 4',49809,'Lingen(Ems)');";
            ExecuteNonQuery(InsertKunde);
            string InsertKunde2 = @"
            INSERT INTO Kunde(Stammkunde,Vorname,Name,Telefon,StrasseNr,Plz,Ort)
VALUES (1,'Viktor','Stelmaszczyk',39493933,'Wiegaldweg 8',49809,'Lingen(Ems)');";
            ExecuteNonQuery(InsertKunde2);

           string SQLFahrt = @"
            INSERT INTO Fahrt(Kennzeichen,PersonalNr,Datum,Preis,Dauer,Reisestart,Reiseziel)
VALUES ('EL-BS 103',1,'2013-06-21',79.99,90,'Lingen','Amsterdam');";
            ExecuteNonQuery(SQLFahrt);




        }
        public List<string> GetAllTableNames()
        {
            List<string> tables = new List<string>();

            string SQL = @"
            SELECT name FROM 
            sqlite_master 
            WHERE type = 'table' 
            ORDER BY 1";

            SQLiteCommand SelectCommand = new SQLiteCommand(_SQLiteConnection);
            SelectCommand.CommandText = SQL;
            SQLiteDataReader reader = SelectCommand.ExecuteReader();
            while(reader.Read())
            {
                if(reader.GetString(0) != "sqlite_sequence")
                tables.Add(reader.GetString(0));
            }
            return tables;
        }
        public List<string> GetAllColumnNames(string tableName)
        {
            List<string> columns = new List<string>();

            string SQL = @"
            SELECT name 
            FROM pragma_table_info($tbname)
            ORDER BY cid;";

            SQLiteCommand getColumnsCommand = new SQLiteCommand(_SQLiteConnection);
            getColumnsCommand.Parameters.AddWithValue("$tbname", tableName);
            getColumnsCommand.CommandText = SQL;

            SQLiteDataReader reader = getColumnsCommand.ExecuteReader();
            while (reader.Read())
            {
                columns.Add(reader.GetString(0));
            }
            return columns;
        }
        public void BackupDatabase()
        {
            _SQLiteConnection.Close();
            File.Copy(DatabaseFile, Environment.CurrentDirectory + "\\Backup.db");
            _SQLiteConnection.Open();
        }
        public void ImportDatabase(string path)
        {
            _SQLiteConnection.Close();
            File.Copy(path, DatabaseFile,true);
            _SQLiteConnection.Open();
        }



    }
}
