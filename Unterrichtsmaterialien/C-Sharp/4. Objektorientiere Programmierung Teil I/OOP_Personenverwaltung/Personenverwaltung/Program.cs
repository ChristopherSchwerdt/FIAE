class Person
{
    public string vorname;
    public string nachname;
    public int alter;

    public void sageEtwas(string text)
    {
        Console.WriteLine($"{vorname} sagt:'{text}'");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Person bob = new Person();
        bob.vorname = "Bob";
        bob.nachname = "Mustermann";
        bob.alter = 12;

        bob.sageEtwas("Hallo leute!");
               

        Person angelika = new Person();
        angelika.vorname = "Angelika";
        angelika.nachname = "Müller";
        angelika.alter = 33;

        angelika.sageEtwas("Hallo Bob!");

        Console.ReadLine();
    }
}