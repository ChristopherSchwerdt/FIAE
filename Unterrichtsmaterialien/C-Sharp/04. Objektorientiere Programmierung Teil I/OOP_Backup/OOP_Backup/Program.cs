
namespace OOP_Backup
{

    class Program
    {
        static void Main(string[] args)
        {
          
            Backup myBackup = new Backup(@"C:\scripts\Downloads\WinSCP-5.21.7-Setup.exe", @"C:\Backup\WinSCP.exe");
            Console.WriteLine("Starte Backup:");
            myBackup.Start();

            
            
            Console.WriteLine("Backup erstellt");
            GC.Collect();
            Console.ReadLine();
        }
    }
    
}