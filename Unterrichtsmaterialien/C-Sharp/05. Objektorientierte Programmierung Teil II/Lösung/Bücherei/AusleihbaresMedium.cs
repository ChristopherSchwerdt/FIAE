namespace Bücherei
{
    internal class AusleihbaresMedium
    {
        // Attribute
        public bool istAusgeliehen;
        private DateTime ausgeliehenBis;

        public string name;

        // Konstruktor
        public AusleihbaresMedium()
        {
            istAusgeliehen = false;
            ausgeliehenBis = DateTime.MinValue; // Standardwert: keine Ausleihung
        }

        // Methode zur Prüfung, ob das Medium ausgeliehen werden kann
        public virtual bool KannAusgeliehenWerden(DateTime rueckgabedatum)
        {
            // Prüfen, ob das Medium ausgeliehen ist
            if (istAusgeliehen)
            {
                //Medium ist bereits verliehen!
                return false;
            }
            else
            {
                // Prüfen, ob die Ausleihfrist überschritten werden würde (30 Tage ab Heute)
                if (rueckgabedatum > DateTime.Now.AddDays(30))
                {   
                    //Rückgabedatum würde überschritten werden.
                    return false;
                }
                else
                {
                    // Medium ist verfügbar und Rückgabe wäre im 30Tage zeitraum!
                    return true;
                }
            }

           
        }

        // Methode, um das Medium bis zu einem bestimmten Datum auszuleihen
        public void AusleihenBis(DateTime ausleihDatum)
        {
            if (KannAusgeliehenWerden(ausleihDatum))
            {
                istAusgeliehen = true;
                ausgeliehenBis = ausleihDatum;
                Console.WriteLine($"Medium erfolgreich bis {ausleihDatum.ToShortDateString()} ausgeliehen.");
            }
            else
            {
                Console.WriteLine("Das Medium kann derzeit nicht ausgeliehen werden.");
            }
        }

    }
}
