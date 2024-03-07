using System.Net.Http.Headers;

namespace CheckEmail
{
    class Program
    {
        static void Main(string[] args) {
            Console.WriteLine("Bitte gib deine E-Mailadresse ein:");
            string Email = Console.ReadLine();
            bool isValid=ValidateEmailAdress(Email);
            Console.WriteLine("Die eingegebene Adresse ist " + ((isValid) ? "gültig." : "nicht gültig!"));
        }

        private static bool ValidateEmailAdress(string email)
        {
           if(email.Contains('@'))
            {
                return true;
            }
           else
            {
                return false;
            }
        }
    }
}