namespace FOR
{
    class Program
    {
        //Alle natürlichen Zahlen von 1 bis 100 summieren.
        static void Main(string[] args)
        {
            int sum = 0;

            for ( int i=1;i <= 100; i++ )
            {
                sum = sum + i;
                // sum += i;
               
            }
            Console.WriteLine(sum);
        }
    }
}