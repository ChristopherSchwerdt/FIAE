//Basisklasse "Animal"
public class Animal
{
    public string Name { get; set; }

    public Animal(string name)
    {
        Name = name;
    }
    public virtual void MakeSound()
    {
        Console.WriteLine("Animal makes a sound");
    }
}
//Abgeleitete Klasse Hund
public class Dog : Animal
{
    public Dog(string name) : base(name)
    {       
    }
    public override void MakeSound()
    {
        Console.WriteLine("Wuff!Wuff!");
    }

}
//Abgeleitete Klasse Katze
public class Cat : Animal
{
    public Cat(string name) : base(name)
    {
    }
    public override void MakeSound()
    {
        Console.WriteLine("Miau!Miau!");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Dog hund = new Dog("Luna");
        Cat katze = new Cat("Sternchen");

        hund.MakeSound();//Gibt "Wuff!Wuff!" aus
        katze.MakeSound();//Gibt "Miau!Miau!" aus

    }
}