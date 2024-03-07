namespace CheckEmail
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hallo! Bitte gib deine E-Mailaddresse ein:");
            string email = Console.ReadLine();
            bool isValid = ValidateEmailAddress(email);

            Console.WriteLine("Die eingegebene Addresse ist " + ((isValid) ? "gültig" : "nicht gültig"));
        }
        static bool ValidateEmailAddress(string email)
        {
            if(email.Contains("@"))
            {
                if(email.Contains(".")) 
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}