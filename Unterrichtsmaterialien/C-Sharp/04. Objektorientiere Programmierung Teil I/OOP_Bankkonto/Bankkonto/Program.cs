class Bankkonto
{
    public string vorname { get; set; }
    public string nachname { get; set; }

    private int _kontonummer;
    private int _kontostand;
    private int _limit;

    public Bankkonto(int kontostand,int limit)
    {
        _kontostand = kontostand;
        _limit = limit;
        _kontonummer = NeueKontonummer();
    }
    private int NeueKontonummer()
    {
       Random myRandom = new Random();
       return myRandom.Next(1000000, 9999999);
    }
    public void AusgabeKontodaten()
    {
        Console.WriteLine("--------------------------");
        Console.WriteLine("Vorname:" + vorname);
        Console.WriteLine("Nachname:" + nachname);
        Console.WriteLine("Kontonummer:" +_kontonummer.ToString());
        Console.WriteLine("Limit:"+ _limit.ToString());
        Console.WriteLine("--------------------------");

    }
    public void Einzahlung(int betrag)
    {
        Console.WriteLine($"Es wurde {betrag} Euro auf Ihr Konto Eingezahlt!");
        _kontostand += betrag;
    }
    public void Auszahlung(int betrag)
    {
        int neuerKontostand = _kontostand - betrag;
        if (neuerKontostand < _limit)
        {
            //Limit überzogen!
            Console.WriteLine("ACHTUNG: LIMIT ÜBERZOGEN! KEINE AUSZAHLUNG!");
        }
        else
        {
            Console.WriteLine($"Es wurden {betrag} Euro von Ihrem Konto Ausgezahlt!");
            _kontostand = neuerKontostand;
            
        }

    }
    public int Kontostand()
    {
        return _kontostand;
    }

}

class Program
{
    static void Main(string[] args)
    {
        Bankkonto konto = new Bankkonto(1000, -500);
        konto.vorname = "Hans";
        konto.nachname = "Müller";

        konto.AusgabeKontodaten();
        Console.WriteLine("Kontostand:"+konto.Kontostand().ToString()+ " Euro");
        konto.Auszahlung(1300);
        Console.WriteLine("Kontostand:" + konto.Kontostand().ToString() + " Euro");
        konto.Auszahlung(1300);
        Console.WriteLine("Kontostand:" + konto.Kontostand().ToString() + " Euro");
        konto.Einzahlung(2000);
        Console.WriteLine("Kontostand:" + konto.Kontostand().ToString() + " Euro");
    }
}
    