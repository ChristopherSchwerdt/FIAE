/// <summary>
/// Basisklasse Zeit, diese kann nicht als Objekt selbst instanziert werden!
/// Das "abstract" Schlüsselwort gibt an, dass es beabsichtigt ist, diese Klasse
/// zu vererben. 
/// </summary>
public abstract class Zeit
{
    protected DateTime _zeit;

    public Zeit()
    {
        _zeit = DateTime.Now;
    }
    public Zeit(DateTime zeit)
    {
        _zeit = zeit;
    }
    //Abstracte Methode. Diese MUSS beim erben überschrieben werden.
    public abstract void ZeitAnzeige();
    
}
//Abgeleitete Klasse ZeitKurz
public class ZeitKurz : Zeit
{
    //Mit dem Schlüsselwort "override" wird die geerbte Methode überschrieben.
    public override void ZeitAnzeige()
    {
        Console.WriteLine("Die Zeit ist: " + _zeit.ToShortTimeString());
    }

}
//Abgeleitete Klasse ZeitLang
public class ZeitLang : Zeit
{
    //Mit dem Schlüsselwort "override" wird die geerbte Methode überschrieben.
    public override void ZeitAnzeige()
    {
        Console.WriteLine("Die Zeit ist: " + _zeit.ToLongTimeString());
    }
}

class Program
{
    static void Main(string[] args)
    {
        //Kann nicht erstellt werden, da abstrakte Klassen
        //selbst nicht instanziert werden können!
        //Zeit myTime = new Zeit();

        //Abgeleitete Klasse erstellen
        ZeitKurz myShortTime = new ZeitKurz();
        //überschriebene Methode ausführen
        myShortTime.ZeitAnzeige();
        //Abgeleitete Klasse erstellen
        ZeitLang myLongTime = new ZeitLang();
        //überschriebene Methode ausführen
        myLongTime.ZeitAnzeige();

    }
}