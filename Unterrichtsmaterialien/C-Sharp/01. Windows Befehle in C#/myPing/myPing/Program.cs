using System.Net.NetworkInformation;
using System.Timers;

namespace myPingApp
{
    class Program
    {
        private static string addressToPing;
        private static int numberOfPings = 4;
        private static int pingCounter;
        private static int successfullyPackets;
        private static int failedPackets;
        private static long minTime = 100000;
        private static long maxTime;
        private static long totalTime;
        static void Main(string[] args)
        {
            System.Timers.Timer myTimer = new System.Timers.Timer();
            myTimer.Elapsed += new System.Timers.ElapsedEventHandler(executePing);
            myTimer.Interval = 1000;
            myTimer.Enabled = true;

            addressToPing = args[0];

            writeHead();

            Console.ReadKey();

        }
        private static void showSummary()
        {
            Ping myPing = new Ping();
            byte[] packet = new byte[32];

            PingReply myReply = myPing.Send(addressToPing, 1000, packet);
            double percentLost= (failedPackets/pingCounter) * 100;
            Console.WriteLine();
            Console.WriteLine("Ping-Statistik für {0}:",myReply.Address);
            Console.WriteLine("    Pakete: Gesendet = {0}, Empfangen = {1}, Verloren = {2}",pingCounter,successfullyPackets,failedPackets);
            Console.WriteLine("    ({0}% Verlust),",percentLost);
            Console.WriteLine("Ca. Zeitangaben in Millisek.:");
            Console.WriteLine("    Minimum = {0}ms, Maximum = {1}ms, Mittelwert = {2}ms",minTime,maxTime,(totalTime/pingCounter));
        }
        private static void writeHead()
        {
            Ping myPing = new Ping();
            byte[] packet = new byte[32];

            PingReply myReply = myPing.Send(addressToPing, 1000, packet);

            Console.WriteLine();
            Console.WriteLine("Ping wird ausgeführt für {0} [{1}] mit {2} Bytes Daten:",addressToPing,myReply.Address,packet.Length);
        }

         static void executePing(object source, ElapsedEventArgs e)
        {
            if (pingCounter <  numberOfPings)
            {
                Ping myPing = new Ping();
                byte[] packet = new byte[32];

                PingReply myReply = myPing.Send(addressToPing, 1000, packet);

                if (myReply.Status == IPStatus.Success)
                {
                    if(myReply.RoundtripTime > maxTime)
                        maxTime = myReply.RoundtripTime;
                    if(myReply.RoundtripTime< minTime)
                        minTime = myReply.RoundtripTime;
                    totalTime = totalTime + myReply.RoundtripTime;
                    successfullyPackets++;

                    try
                    {
                        Console.WriteLine("Antwort von {0}: Bytes={1} Zeit={2}ms TTL={3}", myReply.Address.ToString(), packet.Length, myReply.RoundtripTime, myReply.Options.Ttl);
                    }

                    catch
                    {
                        Console.WriteLine("Fehler!");
                    }

                }
                else
                {
                    failedPackets++;
                }

                pingCounter++;
            }
            else
            {
                showSummary();
                quitApp();
            }
            
           

        }

        private static void quitApp()
        {
            Environment.Exit(0);
        }
    }
}








