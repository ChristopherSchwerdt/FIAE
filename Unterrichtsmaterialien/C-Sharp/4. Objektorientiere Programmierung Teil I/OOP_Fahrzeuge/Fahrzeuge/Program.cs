class Fahrzeug
{
    public string hersteller { get; set; }
    public string modell { get; set; }
    private int _anzahlTueren;

    public int anzahlTueren
    {
        get
        {
            return _anzahlTueren;
        }
        set
        {
            if(value > 0)
            {
                _anzahlTueren = value;
            }
        }
    }


}

class Program
{
    static void Main(string[] args)
    {
        Fahrzeug auto = new Fahrzeug();
        auto.hersteller = "VW";
        auto.modell = "Sharan";
        auto.anzahlTueren = -1;

        Console.WriteLine("Türen:"+auto.anzahlTueren.ToString());
    }
}
