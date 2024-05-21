namespace Buecherdatenbank
{
    class Program
    {
        private static Datenbank datenbank;

        public static void Main(string[] args)
        {
            //Ein neues Objekt von Typ Datenbank erstellen
            datenbank = new Datenbank();
            //Datenbank öffnen
            datenbank.Open();

            ZeigeHauptmenue();

            Console.ReadLine();
            //Datenbank schließen
            datenbank.Close();
        }

        private static void ZeigeHauptmenue()
        {
            Console.Clear();
            Console.WriteLine("Willkommen in der Bücherdatenbank");
            Console.WriteLine();
            Console.WriteLine("Bitte wählen Sie eine Option:");
            Console.WriteLine("1 - Alle Bücher anzeigen");
            Console.WriteLine("2 - Buch hinzufügen");
            Console.WriteLine("3 - Nach ISBN suchen");
            Console.WriteLine("4 - Buchdaten aktualisieren");
            Console.WriteLine("5 - Nach Buch / Büchern suchen");
            Console.WriteLine("0 - Programm beenden");
            Console.WriteLine("");

            int Auswahl = Convert.ToInt32(Console.ReadLine());
            if (Auswahl == 1)
            {
                AlleBuecherAnzeigen();
            }
            if (Auswahl == 2)
            {
                EinBuchHinzufuegen();
            }
            if(Auswahl == 3)
            {
                NachISBNSuchen();
            }
            if(Auswahl == 4)
            {
                BuchdatenAktualisieren();
            }
            if(Auswahl == 5)
            {
                BuchSuche();
            }
            if (Auswahl == 0)
            {
                Environment.Exit(0);
            }
            
        }

        private static void BuchSuche()
        {
            Console.Clear();
            Console.WriteLine("Nach was wollen Sie suchen?");
            Console.WriteLine("1 - Titel");
            Console.WriteLine("2 - Autor");
            Console.WriteLine("3 - Erscheinungsjahr");
            Console.WriteLine("0 - Abbrechen / zurück");

            int Auswahl = Convert.ToInt32(Console.ReadLine());
            //Suche nach einem Titel
            Console.Clear();
            if(Auswahl == 1)
            {
                //Eingabe des Suchbegriffs
                Console.Write("Titel:");
                string suchTitel = Console.ReadLine();
                //Neue Liste von Buechern erstellen
                List<Buch> buecher = new List<Buch>();
                //Datenbank nach Buechern fragen
                buecher = datenbank.GetBooksByTitle(suchTitel);
                ZeigeBuecher(buecher);
            }
            if (Auswahl == 2)
            {
                //Eingabe des Suchbegriffs
                Console.Write("Autor:");
                string suchAutor = Console.ReadLine();
                //Neue Liste von Buechern erstellen
                List<Buch> buecher = new List<Buch>();
                //Datenbank nach Buechern fragen
                buecher = datenbank.GetBooksByAuthor(suchAutor);
                ZeigeBuecher(buecher);
            }
            if (Auswahl == 3)
            {
                //Eingabe des Suchbegriffs
                Console.Write("Erscheinungsjahr:");
                int suchJahr = Convert.ToInt32(Console.ReadLine());
                //Neue Liste von Buechern erstellen
                List<Buch> buecher = new List<Buch>();
                //Datenbank nach Buechern fragen
                buecher = datenbank.GetBooksByYear(suchJahr);
                ZeigeBuecher(buecher);
            }
            if (Auswahl == 0 )
            {
                //Zurück zum Hauptmenü
                ZeigeHauptmenue();
            }



        }

        private static void ZeigeBuecher(List<Buch> buecher)
        {
            Console.Clear();
            Console.WriteLine("Suchergebnis:");
            
            foreach(Buch b in buecher)
            {
                Console.Write(b.titel + " " );
                Console.Write(b.autor + " " );
                Console.Write(b.verlag + " ");
                Console.Write(b.erscheinungsjahr + " ");
                Console.Write(b.isbn + " ");
                Console.WriteLine();
            }
            Console.ReadLine();
            ZeigeHauptmenue();
        }

        private static void BuchdatenAktualisieren()
        {
            // ISBN eingeben:
            Console.Clear();
            Console.Write("ISBN:");
            long isbn = Convert.ToInt64(Console.ReadLine());

            Buch buch = datenbank.SearchForISBN(isbn);
            Console.Clear();
            //Aktueller Titel: Harry Potter
            Console.WriteLine("Aktueller Titel: "+buch.titel);
            Console.WriteLine("(Enter wenn Titel so bleiben soll)");
            Console.Write("Titel:");
            string neuerTitel= Console.ReadLine();
            //Wenn nichts eingegeben worden ist, bleibt der Titel so wie er ist
            if (neuerTitel == "")
            {
                neuerTitel = buch.titel;
            }
            Console.Clear();
            // Aktueller Autor: J.K. Roling
            Console.WriteLine("Aktueller Autor: "+ buch.autor);
            Console.WriteLine("(Enter wenn Autor so bleiben soll)");
            Console.Write("Autor:");
            string neuerAutor= Console.ReadLine();
            //Wenn nichts eingegeben worden ist, bleibt der Autor so wie er ist
            if (neuerAutor == "")
            {
                neuerAutor = buch.autor; 
            }
            Console.Clear();

            //Aktueller Verlag: Pottermore
            Console.WriteLine("Aktueller Verlag:" + buch.verlag);
            Console.WriteLine("(Enter wenn Verlag so bleiben soll)");
            Console.Write("Verlag:");
            string neuerVerlag = Console.ReadLine();
            //Wenn nichts eingegeben worden ist, bleibt der Autor so wie er ist
            if (neuerVerlag == "")
            {
                neuerVerlag = buch.verlag;
            }
            Console.Clear();


            //Aktuelles Erscheinungsjahr: 2005
            Console.WriteLine("Aktuelles Erscheinungsjahr:" + buch.erscheinungsjahr);
            Console.WriteLine("(Enter wenn Erscheinungsjahr so bleiben soll)");
            Console.Write("Erscheinungsjahr:");
            int neuesErscheinungsjahr;
            try
            {
                 neuesErscheinungsjahr = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception ex)
            {
                neuesErscheinungsjahr = buch.erscheinungsjahr;
            }
            Console.Clear();
            //Die "neuen" Werte in die Datenbank speichern.
            Buch aktualisiertesBuch = new Buch();
            aktualisiertesBuch.isbn = isbn;
            aktualisiertesBuch.titel = neuerTitel;
            aktualisiertesBuch.autor = neuerAutor;
            aktualisiertesBuch.erscheinungsjahr = neuesErscheinungsjahr;
            aktualisiertesBuch.verlag = neuerVerlag;

            datenbank.UpdateBook(aktualisiertesBuch);
            

            Console.WriteLine("Buch aktualisiert!");
            Console.ReadLine();
            ZeigeHauptmenue();
            //Gespeichert!


        }

        private static void NachISBNSuchen()
        {
            Console.Clear();
            Console.Write("ISBN:");
            long isbn = Convert.ToInt64(Console.ReadLine());

            Buch buch = datenbank.SearchForISBN(isbn);

            Console.WriteLine("Folgendes Buch wurde gefunden:");
            Console.WriteLine("Titel: " + buch.titel);
            Console.WriteLine("Autor: " + buch.autor);
            Console.WriteLine("Verlag: " + buch.verlag);
            Console.WriteLine("Erscheinungsjahr:" + buch.erscheinungsjahr.ToString());

            Console.ReadLine();
            ZeigeHauptmenue();

        }

        //Ein Buch hinzufügen
        private static void EinBuchHinzufuegen()
        {
            Console.Clear();
            Console.Write("ISBN:");
            long iSBN = Convert.ToInt64(Console.ReadLine());
            Console.Clear();
            Console.Write("Titel:");
            string titel = Console.ReadLine();
            Console.Clear();
            Console.Write("Autor:");
            string autor = Console.ReadLine();
            Console.Clear();
            Console.Write("Verlag:");
            string verlag = Console.ReadLine();
            Console.Clear();
            Console.Write("Erscheinungsjahr:");
            int erscheinungsjahr = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            datenbank.AddBook(iSBN, titel, autor, verlag, erscheinungsjahr);

            Console.WriteLine("Buch hinzugefügt!");
            Console.ReadLine();
            ZeigeHauptmenue();

        }
        //Zeigt alle Bücher der Datenbank an
        private static void AlleBuecherAnzeigen()
        {
            Console.Clear();
            Console.WriteLine("Folgende Bücher stehen Ihnen zur Verfügung:");
            foreach (string book in datenbank.GetAllBooks())
            {
                Console.WriteLine("Titel: " + book);
            }
            Console.ReadLine();
            ZeigeHauptmenue();
        }
    }
}

