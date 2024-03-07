namespace Fahrtenschreiber
{
    class Program
    {
        static int countL = 0;
        static int countR=0;
        static void Main(string[] args)
        {
            Console.WriteLine("Bitte geben Sie die abbiegevorgänge nach Rechts(R) sowie nach Links(L) an:");
            char[] lR = Console.ReadLine().ToCharArray();

            foreach (char c in lR)
            {
                if (c == 'L' || c == 'l')
                {
                    countL++;
                }
                else if(c =='R' || c == 'r')
                {
                    countR++;
                }
            }
            Console.WriteLine($"Es wurde {countR}x nach rechts abgebogen und {countL}x nach links.");

        }
    }
}