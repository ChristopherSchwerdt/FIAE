using Chat_Server;
class Program
{
    static void Main(string[] args)
    {
        ChatServer myServer = new ChatServer(50000);
        myServer.Start();
        Console.WriteLine("Chat-Server gestartet!");
        Console.ReadLine();
    }
}