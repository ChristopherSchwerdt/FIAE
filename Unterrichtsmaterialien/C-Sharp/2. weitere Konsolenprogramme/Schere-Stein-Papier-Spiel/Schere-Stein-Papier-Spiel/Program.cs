

using System;

namespace Game
{
    class Program
    {
        enum Hand
        {
            Schere = 1,
            Stein = 2,
            Papier =3
        }
        static Random myRandom = new Random();
        static Hand playerHand;
        static Hand enemyHand;
        static int playerScore;
        static int enemyScore;

        static void Main(string[] args)
        {
            for(int i =1;i<=3;i++)
            {
                Console.WriteLine("Bitte wähle: Schere(1), Stein(2), Papier(3)");
                playerHand = (Hand)Convert.ToInt32(Console.ReadLine());
                enemyHand = (Hand)myRandom.Next(1, 3);
                Console.WriteLine($"Du hast {playerHand.ToString()} gewählt!");
                Console.WriteLine($"Dein Gegner hat {enemyHand.ToString()} gewählt!");

                //Wenn Spieler Schere hat...
                if (playerHand == Hand.Schere)
                {
                    //... und Gegner Stein
                    if (enemyHand == Hand.Stein)
                    {
                        Console.WriteLine("Der Gegener hat diese Runde gewonnen!");
                        enemyScore++;
                    }
                    //... und Gegner Papier
                    if (enemyHand == Hand.Papier)
                    {
                        Console.WriteLine("Du hast diese Runde gewonnen!");
                        playerScore++;
                    }
                    //... und Gegner auch Schere
                    if (enemyHand == Hand.Schere)
                    {
                        Console.WriteLine("Diese Runde war unentschieden!");

                    }
                }
                if (playerHand == Hand.Stein)
                {
                    //... und Gegner Stein
                    if (enemyHand == Hand.Stein)
                    {
                        Console.WriteLine("Diese Runde war unentschieden!");

                    }
                    //... und Gegner Papier
                    if (enemyHand == Hand.Papier)
                    {
                        Console.WriteLine("Der Gegener hat diese Runde gewonnen!");
                        enemyScore++;

                    }
                    //... und Gegner auch Schere
                    if (enemyHand == Hand.Schere)
                    {
                        Console.WriteLine("Du hast diese Runde gewonnen!");
                        playerScore++;

                    }
                }
                if (playerHand == Hand.Papier)
                {
                    //... und Gegner Stein
                    if (enemyHand == Hand.Stein)
                    {
                        Console.WriteLine("Du hast diese Runde gewonnen!");
                        playerScore++;
                    }
                    //... und Gegner Papier
                    if (enemyHand == Hand.Papier)
                    {
                        Console.WriteLine("Diese Runde war unentschieden!");
                    }
                    //... und Gegner auch Schere
                    if (enemyHand == Hand.Schere)
                    {
                        Console.WriteLine("Der Gegener hat diese Runde gewonnen!");
                        enemyScore++;

                    }
                }
                Console.ReadLine();
                Console.Clear();
            }
            

            Console.WriteLine($"Du hast {playerScore} von 3 Runden gewonnen.");
            Console.WriteLine($"Dein Gegner hat {enemyScore} von 3 Runden gewonnen.");
            Console.WriteLine($"Es gab {3-(enemyScore+playerScore)} unentschieden!");
            Console.WriteLine();
            if (playerScore > enemyScore)
            {
                Console.WriteLine("Du hast das Spiel gewonnen!");
            }
            if (enemyScore > playerScore)
            {
                Console.WriteLine("Dein Gegner hat das Spiel gewonnen!");
            }
        }
    }
}