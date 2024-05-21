using System.Runtime.CompilerServices;

namespace Encrypt
{
    class Program
    {
        static char[] validCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ.abcdefghijklmnopqrstuvwxyz".ToCharArray();
        static string message;
        static string encryptedMessage;
        static void Main(string[] args)
        {
            ////Verschlüsseln
            //Console.WriteLine("Bitte geben Sie etwas zum Verschlüsseln ein:");
            //message = Console.ReadLine();
            //Console.WriteLine("Verschlüsselte Botschaft:");
            //encryptedMessage = Encrypt(message, 3);
            //Console.WriteLine(encryptedMessage);
            ////Entschlüsseln
            //Console.WriteLine("Wieder entschlüsselt:");
            //Console.WriteLine(Decrypt(encryptedMessage, 3));


            //1. Abfrage ob die Datei Ver- oder Entschlüsselt werden soll.
            Console.WriteLine("Soll die Datei Verschlüsselt(1) oder Entschlüsselt(2) werden?");
            int input = Convert.ToInt32(Console.ReadLine());
            //1a. Schlüssel abfragen.
            Console.WriteLine($"Bitte den Schlüssel eingeben(1-{validCharacters.Length}:)");
            int key = Convert.ToInt32(Console.ReadLine());

            if(input == 1)
            {
                EncryptFile(key);
            }
            

            //3. Datei entschlüsseln
            //3a. Datei auslesen und in eine String-Variable speichern.
            //3b. String-Variable entschlüsseln.
            //3c. String-Variable wieder in Datei schreiben.



        }
        //2. Datei Verschlüsseln
        private static void EncryptFile(int key)
        {

            string message = "";
            //2a. Datei auslesen und in eine String-Variable(message) speichern
            StreamReader myStreamReader = new StreamReader("secret.txt");

            message = myStreamReader.ReadToEnd();
            //Streamreader schließen.
            myStreamReader.Close();

            //2b. String-Variable verschlüsseln.
            message = Encrypt(message, key);
            //2c. String-Variable wieder in Datei schreiben.
            StreamWriter myStreamWriter = new StreamWriter("secret.txt");
           
            myStreamWriter.Write(message);
            myStreamWriter.Close();

        }

        private static string Encrypt(string message,int key)
        {
            string secretMessage = "";
            
            
            foreach(char c in message)
            {
                if(validCharacters.Contains(c))
                {
                    int positionOfChar = Array.IndexOf(validCharacters, c);
                    int newPositionOfChar = positionOfChar + key;
                    int remain = newPositionOfChar % validCharacters.Length;
                    secretMessage += validCharacters[remain];
                }
                else
                {
                    secretMessage += c;
                }

            }

            return secretMessage;
        }
        private static string Decrypt(string message, int key)
        {
            string secretMessage = "";
            

            foreach (char c in message)
            {
                if (validCharacters.Contains(c))
                {
                    int positionOfChar = Array.IndexOf(validCharacters, c);
                    int newPositionOfChar = positionOfChar - key;
                    int remain = newPositionOfChar % validCharacters.Length;
                    secretMessage += validCharacters[remain];
                }
                else
                {
                    secretMessage += c;
                }

            }

            return secretMessage;
        }
    }
}