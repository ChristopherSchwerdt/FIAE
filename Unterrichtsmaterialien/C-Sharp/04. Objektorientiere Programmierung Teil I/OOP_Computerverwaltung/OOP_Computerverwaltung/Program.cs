class Computer
{
    private bool hdmiVorhanden;
    private int festplattengroeße;
    public bool setzeHDMI
    {
        get
        {
            return hdmiVorhanden;
        }
        set
        {
            hdmiVorhanden = value;
        }
    }
    public int SetzeFestplattenGröße
    {
        get 
        { 
            return festplattengroeße; 
        }
        set
        {
            if (value >= 32 && value <= 4096)
            {
                festplattengroeße = value;
            }
            
        }
    }
    public void ZeigeInfo()
    {
        Console.WriteLine("hardwareinfo:");
        Console.WriteLine($"Die Festplatte ist {festplattengroeße} GB groß");
        if (hdmiVorhanden)
            Console.WriteLine("ein HDMI-Anschluss ist vorhanden!");
    }

}
class Program
{
    static void Main(string[] args)
    {
        Computer workstation = new Computer();
        workstation.SetzeFestplattenGröße = 256;
        workstation.setzeHDMI = false;
        workstation.ZeigeInfo();

        Computer notebook = new Computer();
        notebook.SetzeFestplattenGröße = 128;
        notebook.setzeHDMI = true;
        notebook.ZeigeInfo();


    }
}

