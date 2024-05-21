class Movie
{
    public string titel;
    public int altersbeschraenkung;
    public Kategorie kategorie;
}

class MoviePlatform
{
    static void Main(string[] args)
    {

        Movie film1 = new Movie();
        film1.titel = "Barbie auf dem Ponyhof";
        film1.altersbeschraenkung = 0;
        film1.kategorie = Kategorie.Kinder;

        Console.WriteLine("Name:" + film1.titel);
        Console.WriteLine("Alterfreigabe:"+film1.altersbeschraenkung);
        Console.WriteLine("Kategorie:"+film1.kategorie.ToString());

        Console.WriteLine("------------------------");

        Movie film2 = new Movie();
        film2.titel = "Avatar";
        film2.altersbeschraenkung = 12;
        film2.kategorie = Kategorie.Fantasy;
        Console.WriteLine("Name:" + film2.titel);
        Console.WriteLine("Alterfreigabe:" + film2.altersbeschraenkung);
        Console.WriteLine("Kategorie:" + film2.kategorie.ToString());

    }
}
enum Kategorie
{
    Romantik,
    Action,
    Fantasy,
    Kinder
}