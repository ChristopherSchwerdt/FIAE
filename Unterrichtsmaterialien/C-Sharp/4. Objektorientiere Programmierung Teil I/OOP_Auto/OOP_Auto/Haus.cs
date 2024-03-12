
namespace OOP
{
    class Haus
    {
        private int anzahlSchornsteine;

        public Haus()
        {
            anzahlSchornsteine = 1;
            Console.WriteLine("Neues Objekt(Typ Haus) erstell!");
        }
        public Haus(int anzahlSchornsteine)
        {
            this.anzahlSchornsteine = anzahlSchornsteine;
        }

        ~Haus()
        {
            Console.WriteLine("Objekt (vom Typen Haus) zerstört!");
        }
    }
}
