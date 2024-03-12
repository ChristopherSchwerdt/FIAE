class Auto
{
    public string fahrzeugMarke;
    public string fahrzeugModel;
    public int anzahlRaeder;
    public int preis;
    public int kraftstoffArt;
    private int anzahlTueren;
    public int AnzahlTueren
    {
        get 
        {
            return anzahlTueren;
        }
        set
        {
            if (value > 0 && value <= 5)
                anzahlTueren = value;
        }
    }

    public void Beschleunigen(int strecke) { }
    public void Bremsen() { }
    public int GetLuftDruck(int Reifen) { return 0; }

   

}

class Program
{
    static void Main(string[] args)
    {
        Auto neuesAuto = new Auto();
        neuesAuto.fahrzeugMarke = "VW";
        neuesAuto.fahrzeugModel = "Sharan";
        neuesAuto.Beschleunigen(100);
    }
}

