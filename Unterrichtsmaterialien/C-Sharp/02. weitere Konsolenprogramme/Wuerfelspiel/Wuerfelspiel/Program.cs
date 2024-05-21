namespace Dicegame
{
    class Program
    {       
        //Erzeugen eines Zufallgenerators
        static Random myRandom = new Random();
        //Variablen für die aktuelle Runde
        static int currentNumberCMP = 0;
        static int currentNumberPLR = 0;
        //Variablen die den Score zählen
        static int playerScore = 0;
        static int computerScore = 0;
        
        static void Main(string[] args)
        {
            //For-Schleife für die Runden
            for(int i = 1; i <= 10; i++)
            {
                Console.WriteLine($"Runde{i}:");
                currentNumberCMP = myRandom.Next(1, 6);
                Console.WriteLine($"Der Computer hat eine {currentNumberCMP} gewürfelt!");
                Console.WriteLine("Drücke eine beliebige Taste zum Würfeln");
                Console.ReadKey();
                currentNumberPLR = myRandom.Next(1, 6);
                Console.WriteLine($"Du hast eine {currentNumberPLR} gewürfelt!");
                
                //Wenn der Computer höher als der Spieler gewüfelt hat
                if (currentNumberCMP > currentNumberPLR)
                {
                    Console.WriteLine("Der Computer hat die Runde gewonnen!");
                    computerScore++;
                }
                //Wenn der Spieler höher als der Computer gewürfel hat
                if (currentNumberPLR > currentNumberCMP)
                {
                    Console.WriteLine("Du hast die Runde gewonnen!");
                    playerScore++;
                }
                //Bei einem unentschieden
                if (currentNumberPLR == currentNumberCMP)
                {
                    Console.WriteLine("Unentschieden!");
                    
                }
                //Beliebige Taste für die nächste Runde drücken
                Console.ReadLine();
                Console.Clear();
            }//Ende der For-Schleife


            //Zusammenfassung am Ende des Spiels:
            Console.WriteLine();
            Console.WriteLine($"Der Computer hat {computerScore} von 10 Runden gewonnen.");
            Console.WriteLine($"Du hast {playerScore} von 10 Runden gewonnen.");
            //Die unentschieden werden berechnet:
            Console.WriteLine($"Es waren {10-(playerScore+computerScore)} unentschieden.");
            Console.WriteLine();

            //Wer hat das Spiel gewonnen?
            if(computerScore > playerScore)
            {
                Console.WriteLine("Der Computer hat das Spiel gewonnen!");
            }
            if(playerScore > computerScore)
            {
                Console.WriteLine("Du hast das Spiel gewonnen!");
            }
            Console.ReadLine();

        }
    }
}