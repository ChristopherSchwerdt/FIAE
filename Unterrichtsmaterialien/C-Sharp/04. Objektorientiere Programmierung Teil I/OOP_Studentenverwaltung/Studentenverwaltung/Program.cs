class Student
{

    private string _name;
    private string _studiengang;
    public bool istImmatrikuliert { get; set; }

    public void Immatrikulieren()
    {
        istImmatrikuliert = true;
    }
    public Student(string name, string studiengang)
    {

        _name = name;
        _studiengang = studiengang;
    }

}

class Program
{
    static void Main(string[] args)
    {
        Student student = new Student("Hans Peter", "Informatik");

        if (student.istImmatrikuliert)
        {
            Console.WriteLine("ist immatrikuliert!");
        } else
        {
            Console.WriteLine("ist nicht immatrikuliert");
        }

        student.Immatrikulieren();
        if (student.istImmatrikuliert)
        {
            Console.WriteLine("ist immatrikuliert!");
        }
        else
        {
            Console.WriteLine("ist nicht immatrikuliert");
        }

    }
}