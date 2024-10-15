namespace Bücherei
{
    class Software : AusleihbaresMedium
    {
        //Überschreiben der Funktion aus der Basisklasse
        public override bool KannAusgeliehenWerden(DateTime rueckgabedatum)
        {
            // Prüfen, ob das Medium ausgeliehen ist
            if (istAusgeliehen)
            {
                //Medium ist bereits verliehen!
                return false;
            }
            else
            {
                // Prüfen, ob die Ausleihfrist überschritten werden würde (7 Tage ab Heute für Software)
                if (rueckgabedatum > DateTime.Now.AddDays(7))
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
    }
}
