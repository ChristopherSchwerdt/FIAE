using System.IO;
using System.Runtime.CompilerServices;

namespace myDirApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Erzeugt ein Stringarray mit allen Dateien des aktuellen Verzeichnisses
            string[] allFiles = Directory.GetFiles(Directory.GetCurrentDirectory());

            //Gebe das aktuelle Verzeichnis aus:
            Console.WriteLine("Verzeichnis:" + Directory.GetCurrentDirectory()+"\n");
            Console.WriteLine();

            //Für jede Datei im Verzeichnis:
            foreach (string file in allFiles)
            {
                //Erzeuge ein neues FileInfo-Objekt
                FileInfo fileInfo = new FileInfo(file);
                //Letzte Änderung
                Console.Write(File.GetLastWriteTime(file) + "\t");
                //Dateigröße
                Console.Write(fileInfo.Length + "\t");
                //Dateiname
                Console.Write(Path.GetFileName(file));
                //Leerzeile
                Console.WriteLine();
            }

        }
    }
}
