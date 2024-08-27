// Namespace ist wie ein Ordner
// dient zur gliederung des Quellcodes
namespace Funktionen.Tutorial.FunktionenUndMethoden
   
{   //Ein "Bauplan" zur erstellung eines Objektes 
    class MeineTolleKlasse
    {
        //public == offener Zugriff
        //static == statische Klasse kann nicht instanziert werden
        //void == gibt nichts zurück
        //Main == Name der Methode 
        //String[] args == Übergabeparameter die beim starten des Programmes mit 
        //übergeben worden sind können hier im Programm genutzt werden.

        //Haupteinstiegspunkt des Programms
        //Wird als erstes Ausgeführt
        public static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                Console.WriteLine("Der Parameter war: " + args[0] + "," + args[1]);
            }

            Console.WriteLine("Programm gestartet!");

            //Aufrufen der Methode "Sternchen":
            Sternchen();
            Sternchen(17);

            //Aufrufen der Methode "Addiere" mit parametern:
            Addiere(3, 68);
            Addiere(33, 332);

            List<string> list = new List<string>();
            list.Add("Shiggy");

            string sentence = "Ich bin ein Pokemon!";
            char[] arr = sentence.ToCharArray();
            int count = 0;
            for (int i = 0;i<arr.Length;i++)
            {
                if (arr[i] ==' ')
                {
                    count++;
                }
            }

            string[] stringArray = sentence.Split(' ');
           
            foreach(string word in stringArray)
            {
                count++;
            }

            Console.WriteLine("Dieser Satz hat" + count + " Wörter!");

            int eingabeZahl1 = int.Parse(Console.ReadLine());
            int eingabeZahl2 = int.Parse(Console.ReadLine());

            Addiere(eingabeZahl1, eingabeZahl2);

            //Aufrufen der Funktion "GibmirdieGroeßereZahl"
            int gzahl = GibMirDieGroessereZahl(4, 2);

            Console.WriteLine(GibMirDieGroessereZahl(45,12));
            //Per Usereingabe:
            int eingabeZahl3 = int.Parse(Console.ReadLine());
            int eingabeZahl4 = int.Parse(Console.ReadLine());
            Console.WriteLine(GibMirDieGroessereZahl(eingabeZahl3,eingabeZahl4));

            Console.WriteLine("Programm wird beendet!");


        }
        //Sternchen Methode:
        private static void Sternchen()
        {
            //Sternchen ausgeben:
            Console.WriteLine("  *  ");
            Console.WriteLine(" ****");
            Console.WriteLine("******");
            Console.WriteLine(" ****");
            Console.WriteLine("  *");

        }
        //Überladene Methode "Sternchen"
        private static void Sternchen(int anzahl)
        {
            for (int i = 0; i < anzahl; i++)
            {
                Sternchen();
            }
        }
        //Die Methode "Addieren" nimmt zwei Zahlen als Parameter an:
        public static void Addiere(int zahl1, int zahl2)
        {
            int add = zahl1 + zahl2;
            Console.WriteLine(add);
        }
        //Die Funktion "GibMirDieGroessereZahl" nimmt zwei Zahlen als Parameter an
        // und gibt dannach die groeßere von beiden als int zurück(return):
        private static int GibMirDieGroessereZahl(int zahl1, int zahl2)
        {
            //temporäre hilfsvariable
            int zahl;

            if (zahl1 > zahl2)
            {
                zahl = zahl1;
            }
            else
            {
                zahl = zahl2;
            }

            return zahl;
        }
        

    }
}