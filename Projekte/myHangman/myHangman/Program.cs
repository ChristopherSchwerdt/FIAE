
class Program
{
    private static string hangmanWord;   
    private static char myChar;
    private static string guessWord;
    private static bool isSolved = false;
    private static List<string> listOfAlreadyCheckedCharacters;
    static void Main(string[] args)
    {

        Console.Title = "Hangman" ;
        listOfAlreadyCheckedCharacters = new List<string>();

        Console.Write("Bitte geben Sie ein Wort ein:");
        hangmanWord = Console.ReadLine();
        Console.Clear();

        guessWord = getUnderscore(hangmanWord);
        
        while(!isSolved)
        {
            gameloop();
        }

        Console.WriteLine("Gewonnen! Das Wort war: " + hangmanWord);
        Console.ReadLine();

    }
    private static void gameloop()
    {

        Console.WriteLine($"Das gesuchte Wort ist:{guessWord}");
        Console.Write("Buchstaben die NICHT im Wort vorkommen:");
        foreach(string Buchstabe in  listOfAlreadyCheckedCharacters)
        {
            Console.Write($"[{Buchstabe}] ");
        }
        Console.WriteLine();

        Console.WriteLine("Bitte gebe einen Buchstaben ein:");

        myChar = Console.ReadKey(true).KeyChar;
        //myChar = Console.ReadLine()[0];
        Console.Clear();
        if(!checkCharacter(myChar))
        {
            if (!listOfAlreadyCheckedCharacters.Contains(myChar.ToString()))
            {
                listOfAlreadyCheckedCharacters.Add(myChar.ToString());
            }
        }

        isSolved= checkIfisSolved();
       
        

    }

    private static bool checkIfisSolved()
    {
        char[] guessWordAsCharArray = guessWord.ToCharArray();
       
        for (int i = 0; i < guessWordAsCharArray.Length; i++)
        {
            if (guessWord[i] == '_')
            {
                return false;
            }
        }

        return true;
    }

    private static bool checkCharacter(char character)
    {
        bool foundAChar = false;
        char[] guessWordAsCharArray = guessWord.ToCharArray();
        char[] hangmanWordAsCharArray = hangmanWord.ToCharArray();

       for(int i=0;i<hangmanWord.Length;i++)
        {
           if (character == hangmanWord[i])
            {
                //Buchstabe ist im Wort enthalten!
                guessWordAsCharArray[i] = character;
                foundAChar = true;
            }
        }
        if(foundAChar)
        {
            guessWord = new string(guessWordAsCharArray);
        }
        return foundAChar;

    }
    private static string getUnderscore(string word)
    {
        string underscoreString = "";

        foreach (char c in word)
        {
            underscoreString +=  "_";
        }

        return underscoreString;
    }
}