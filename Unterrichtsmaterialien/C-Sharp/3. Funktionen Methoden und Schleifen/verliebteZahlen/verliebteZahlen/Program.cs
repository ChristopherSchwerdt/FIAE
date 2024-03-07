using System;

namespace verliebteZahlen
{

    class Program
    {
        static void Main(string[] args) {
            
            WriteToConsole("Bitte gib eine Zahl zwischen 1 und 10 ein:");

            int zahl = Convert.ToInt32(Console.ReadLine());

            int verliebteZahl= VerliebteZahl(zahl);

            Console.WriteLine("Die verliebte Zahl von " + zahl + " ist die " + verliebteZahl);

        }

        private static int VerliebteZahl(int zahl)
        {
            return 10 - zahl;
        }

        static void WriteToConsole(string message)
        {
            Console.WriteLine(message);
        }


    }
}