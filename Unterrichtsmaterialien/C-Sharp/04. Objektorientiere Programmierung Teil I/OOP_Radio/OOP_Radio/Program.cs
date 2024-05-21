class Radio
{
    //Die privaten (von außen nicht erreichbaren) Variablen der Klasse Radio:
    private bool _eingeschaltet;
    private int _lautstaerke;
    private double _frequenz;

    //leerer Konstruktor / Standardkonstruktor
    public Radio()
    {

    }
    //überladener Konstruktor
    public Radio(bool istAn,int lautstaerke, double frequenz)
    {
        _eingeschaltet = istAn;
        _lautstaerke = lautstaerke;
        _frequenz = frequenz;

    }
    //Wenn Radio noch nicht auf Lautstärke 10, dann stelle es 1 lauter.
    public void lauter()
    {
        if (_eingeschaltet)
        {
            if(_lautstaerke < 10)
            {
                _lautstaerke++;
                Console.WriteLine("Stelle das Radio lauter. Es ist nun auf: " + _lautstaerke);
            }
        }
    }
    //Wenn Radio noch nicht auf Lautstärke 0, dann stelle es um 1 leiser.
    public void leiser()
    {
        if (_eingeschaltet)
        {
            if (_lautstaerke > 0)
            {
                
                _lautstaerke--;
                Console.WriteLine("Stelle das Radio leiser. Es ist nun auf: " + _lautstaerke);
            }
        }
    }
    //Schalte das Radio ein.
    public void an()
    {
        Console.WriteLine("Schalte Radio ein!");
        _eingeschaltet = true;
    }
    //Schalte das Radio aus.
    public void aus()
    {
        Console.WriteLine("Schalte Radio aus!");
        _eingeschaltet = false;
    }
    //Gebe die aktuellen Zustände des Radios als String zurück
    public string toString()
    {
       if ( !_eingeschaltet )
        {
            return "Radio ist aus!";
        }
       else
        {
            return $"Radio an: Freq={_frequenz},Laut={_lautstaerke}";
        }
    }

    //Stelle den Sender zwischen 85.0 und 110.0 ein, ansonsten
    // auf 99.9 falls dieser ausserhalb des Bereiches liegt:
    public void waehleSender(double frequenz)
    {

        if( frequenz < 110.0 && frequenz > 85.0)
        {
            _frequenz = frequenz;
            Console.WriteLine("Ändere Frequenz auf:" + frequenz);
        }
        else
        {
            _frequenz = 99.9;
            Console.WriteLine("Ungültige frequenz!Setze fallback auf 99.9Mhz!");
        }
    }

}


class Program
{
    static void Main(string[] args)
    {
        //Erstelle ein neues Objekt vom Typ "Radio"
        Radio meinRadio = new Radio(true, 5, 90.4);
    
        meinRadio.aus();
        meinRadio.an();

        Console.WriteLine(meinRadio.toString());

        meinRadio.waehleSender(96.6);
        meinRadio.lauter();
        meinRadio.lauter();
        Console.WriteLine(meinRadio.toString());
    }
}