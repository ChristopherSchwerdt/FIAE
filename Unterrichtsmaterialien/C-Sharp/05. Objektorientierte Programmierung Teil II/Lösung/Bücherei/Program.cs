namespace Bücherei
{
    class Program
    {
        public static void Main(string[] args)
        {
            //Instanz der Klasse "ausleihbaresMedium" erstellen. 
            AusleihbaresMedium ausleihbaresMedium = new AusleihbaresMedium();
            ausleihbaresMedium.name = "AusleihbaresMedium";
            //Die Funktion "KannAusgeliehenWerden" testen...
            Console.WriteLine($"{ausleihbaresMedium.name} kann für 15 Tage ausgeliehen werden:" + ausleihbaresMedium.KannAusgeliehenWerden(DateTime.Now.AddDays(15)));
            Console.WriteLine($"{ausleihbaresMedium.name} kann für 30 Tage ausgeliehen werden:" + ausleihbaresMedium.KannAusgeliehenWerden(DateTime.Now.AddDays(30)));
            Console.WriteLine($"{ausleihbaresMedium.name} kann für 31 Tage ausgeliehen werden:" + ausleihbaresMedium.KannAusgeliehenWerden(DateTime.Now.AddDays(31)));

            //Instanz der Klasse "Video" erstellen.
            Video video = new Video();
            video.name = "Scream 2";
            video.produzent = "Wes Craven";
            //Die Funktion "KannAusgeliehenWerden" testen...
            Console.WriteLine($"{video.name} kann für 15 Tage ausgeliehen werden:" + video.KannAusgeliehenWerden(DateTime.Now.AddDays(15)));
            Console.WriteLine($"{video.name} kann für 30 Tage ausgeliehen werden:" + video.KannAusgeliehenWerden(DateTime.Now.AddDays(30)));
            Console.WriteLine($"{video.name} kann für 31 Tage ausgeliehen werden:" + video.KannAusgeliehenWerden(DateTime.Now.AddDays(31)));

            //Instanz der Klasse "Software" erstellen.
            Software software = new Software();
            software.name = "Microsoft Word";
            //Die Funktion "KannAusgeliehenWerden" testen...
            Console.WriteLine($"{software.name} kann für 4 Tage ausgeliehen werden:" + software.KannAusgeliehenWerden(DateTime.Now.AddDays(4)));
            Console.WriteLine($"{software.name} kann für 30 Tage ausgeliehen werden:" + software.KannAusgeliehenWerden(DateTime.Now.AddDays(30)));
            Console.WriteLine($"{software.name} kann für 31 Tage ausgeliehen werden:" + software.KannAusgeliehenWerden(DateTime.Now.AddDays(31)));

            //Eine Liste von Typ "AusleihbaresMedium" erstellen.
            List<AusleihbaresMedium> AlleAusleihbareMedien = new List<AusleihbaresMedium>();

            //Die oben erstellten Instanzen der Liste hinzufügen.
            AlleAusleihbareMedien.Add(ausleihbaresMedium);
            AlleAusleihbareMedien.Add(video);
            AlleAusleihbareMedien.Add(software);

            //Durch die Liste(alle Medien) iterieren
            foreach(AusleihbaresMedium medium in AlleAusleihbareMedien)
            {
                int Days = 5;
                Console.WriteLine($"{medium.name} kann für {Days} Tage ausgeliehen werden:"+medium.KannAusgeliehenWerden(DateTime.Now.AddDays(Days)));
                Days = 9;
                Console.WriteLine($"{medium.name} kann für {Days} Tage ausgeliehen werden:" + medium.KannAusgeliehenWerden(DateTime.Now.AddDays(Days)));
                Days = 27;
                Console.WriteLine($"{medium.name} kann für {Days} Tage ausgeliehen werden:" + medium.KannAusgeliehenWerden(DateTime.Now.AddDays(Days)));
                Days = 50;
                Console.WriteLine($"{medium.name} kann für {Days} Tage ausgeliehen werden:" + medium.KannAusgeliehenWerden(DateTime.Now.AddDays(Days)));
            }
        }
    }
}