using System.Net.NetworkInformation;
using System.Timers;

namespace myPingApp
{
     class Program
    {
        private static string addressToPing;
        private static int numberOfPings;
        private static int pingCounter;
        private static bool pingInfinite = false;
        private static int bufferSize = 32;
        private static bool dontFragment = false;
        private static int timeToLive = 128;
        private static int timeout = 1000;
        private static int succesfullyPackets = 0;
        private static int lostPackets = 0;
        private static long minTime = 100000;
        private static int timerIntervall = 1000;
        private static long maxTime;
        private static long totalTime;

         static void Main(string[] args)
        {
            //Erstelle einen Timer um die Pingbefehle im Intervall auszuführen
            System.Timers.Timer myTimer = new System.Timers.Timer();
            myTimer.Elapsed += new System.Timers.ElapsedEventHandler(executePing);
            myTimer.Interval = timerIntervall;

            //Wenn Parameter per Kommandozeile übergeben worden sind:
            if (args.Length > 0)
            {
                parseInput(args);
                writeHead();
                //Starte die Pings
                myTimer.Enabled = true;
            }
            else // Wenn keine Kommandozeilenparameter übergeben worden sind:
            {
               
                showProgramInfo();
                //Schließe das Programm
                quitApp();
            }
            //Halte das Programm "aufrecht"... ist hier nötig, da das Programm mit einem Timer als Thread läuft. 
            //Ohne das Readkey, würde das Programm einfach den Timer im Thread starten aber sich dann hier beenden.
            Console.ReadKey();
           
        }
        //Parse die Kommandozeilenparameter
        private static void parseInput(string[] args)
        {
            numberOfPings = 4;
            pingCounter = 0;
            addressToPing = args[0];

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i][0] == '-')
                {
                    if (args[i] == "-t")
                    {
                        pingInfinite = true;
                    }
                    if (args[i].Substring(0, 2) == "-n")
                    {
                        numberOfPings = Int32.Parse(args[i + 1]);
                    }
                    if (args[i].Substring(0, 2) == "-l")
                    {
                        bufferSize = Int32.Parse(args[i + 1]);
                    }
                    if (args[i] == "-f")
                    {
                        dontFragment = true;
                    }
                    if (args[i].Substring(0, 2) == "-i")
                    {
                        timeToLive = Int32.Parse(args[i + 1]);
                    }
                    if (args[i].Substring(0, 2) == "-w")
                    {
                        timeout = Int32.Parse(args[i + 1]);
                    }
                }

            }



        }
        //Zeige dem User mögliche Kommandozeilenparameter an:
        private static void showProgramInfo()
        {
            Console.WriteLine();
            Console.WriteLine("Syntax: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Name +
                " Zielname [-t] [-n count] [-l size] [-f] [-i TTL] [-w timeout] "
                );
            Console.WriteLine();
            Console.WriteLine("Optionen:");
            Console.WriteLine("    -t             Pingt den angegebenen Host bis zur Beendigung des Vorgangs.\r\n" +
                "                   Drücken Sie STRG+UNTBR, um die Statistik anzuzeigen und\r\n" +
                "                   den Vorgang fortzusetzen.\r\n" +
                "                   Drücken Sie STRG+C, um den Vorgang abzubrechen.");
            Console.WriteLine("    -n count       Die Anzahl der zu sendenden Echoanforderungen.");
            Console.WriteLine("    -l size        Die Größe des Sendepuffers.");
            Console.WriteLine("    -f             Legt das Kennzeichen für \"Nicht fragmentieren\" im Paket\r\n" +
                "                   fest (nur IPv4).");
            Console.WriteLine("    -i TTL         Die Lebensdauer.");
            Console.WriteLine("    -w timeout     Zeitlimit in Millisekunden für die einzelnen Antworten.");
        }
        //Erstelle den Ping "Kopf". Hier wird ein erster Versuch durgeführt das Ziel per Ping zu erreichen. 
        //Es wird somit auch der angegebene Host geprüft.Ist der Host nicht valide, wird eine Fehlermeldung
        //angezeigt und das Programm beendet.
        private static void writeHead()
        {

            //Erstelle das Ping Objekt
            Ping myPing = new Ping();
            PingOptions myPingOptions = new PingOptions();
            myPingOptions.DontFragment = dontFragment;
            myPingOptions.Ttl = timeToLive;
            byte[] packet = new byte[bufferSize];

            //Versuche zu den angegebenen Host zu erreichen
            try
            {
                PingReply reply = myPing.Send(addressToPing, timeout, packet,myPingOptions);
                string resolvedAddress = "";
                if (reply.Status == IPStatus.Success)
                    resolvedAddress = " [" + reply.Address.ToString() + "] ";
                Console.WriteLine();
                Console.WriteLine("Ping wird ausgeführt für {0}{1} mit {2} Bytes Daten:", addressToPing, resolvedAddress, packet.Length);
            }
            //Falls es eine Fehlermeldung gibt, fange diese ab und teile es dem User mit. Dann beende das Programm
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                if (!String.IsNullOrEmpty(e.InnerException.Message))
                    Console.WriteLine(e.InnerException.Message);
                Console.WriteLine();
                quitApp();
            }        

        }

        //Zeige am Ende der Pingausführung eine Zusammenfassung. Hier werden die gesendeten, empfangenen und
        //verlorenen Pakete angezeigt.Außerdem erfolgt eine Anzeige der jeweiligen mindest bzw. maximal Zeiten.
        private static void showSummary()
        {
            Ping myPing = new Ping();
            PingOptions myPingOptions = new PingOptions();
            myPingOptions.DontFragment = dontFragment;
            myPingOptions.Ttl = timeToLive;
            byte[] packet = new byte[bufferSize];


            PingReply reply = myPing.Send(addressToPing, timeout, packet,myPingOptions);
            string resolvedAddress = "";

            resolvedAddress = reply.Address.ToString();
            double percentLost = (lostPackets / pingCounter) * 100;
            long averTime = totalTime / pingCounter;
            Console.WriteLine();
            Console.WriteLine("Ping-Statistik für {0}:", resolvedAddress);
            Console.WriteLine("    Pakete: Gesendet = {0}, Empfangen = {1}, Verloren = {2}", pingCounter, succesfullyPackets, lostPackets);
            Console.WriteLine("    ({0}% Verlust),", percentLost);
            if(percentLost < 100)
            {
                Console.WriteLine("Ca. Zeitangaben in Millisek.:");
                Console.WriteLine("    Minimum = {0}ms, Maximum = {1}ms, Mittelwert = {2}ms", minTime, maxTime, averTime);
            }
           
            Console.WriteLine();
        }
        //Beende das Programm
        private static void quitApp()
        {
            Environment.Exit(0);
        }
        //Führe einen Ping durch.
        private static void executePing(object source, ElapsedEventArgs e)
        {
            if (!pingInfinite)
            {
                if (pingCounter == numberOfPings)
                {
                    showSummary();
                    quitApp();
                }

            }


            Ping myPing = new Ping();
            PingOptions myPingOptions = new PingOptions();
            myPingOptions.DontFragment = dontFragment;
            myPingOptions.Ttl = timeToLive;
             

            byte[] packet = new byte[bufferSize];


            PingReply reply = myPing.Send(addressToPing, timeout, packet);
            if (reply.Status == IPStatus.Success)
            {
                // Es werden die Werte für die Statistik berechnet:
                succesfullyPackets++;
                totalTime += reply.RoundtripTime;
                if (reply.RoundtripTime < minTime) { minTime = reply.RoundtripTime; }
                if (reply.RoundtripTime > maxTime) { maxTime = reply.RoundtripTime; }

                Console.WriteLine("Antwort von {0}: Bytes={1} Zeit={2}ms TTL={3}", reply.Address.ToString(), packet.Length, reply.RoundtripTime, reply.Options.Ttl.ToString());
            }
            else
            {
                lostPackets++;
                Console.WriteLine(reply.Status);
            }
            pingCounter++;

        }

    }


}
