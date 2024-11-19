namespace RPG_Inventory_Sort
{
   
    class Game
    {
        static Inventory myInventory;
        static int CurrentSortIndex = 0;
        static string CurrentSortCreteria = "";

        static void Main(string[] args)
        {
            
            //Erstelle die Items mit ihren jeweiligen Werten als Liste
            List<InventoryItem> inventoryItems = new List<InventoryItem>
            {
                new InventoryItem(0,"Schwert der Wiedergeburt", 1, 150, ItemRarity.Selten, 4.5, 73,"Ein legendäres Schwert, geschmiedet aus dem reinsten Mithril und verzaubert \n mit den Seelen der gefallenen Helden. Dieses Schwert hat die Macht,\n seinen Träger nach dem Tod wiederzubeleben, ihn stärker und weiser zu machen.\n Mit jedem Schlag leuchtet die Klinge hell und füllt den Träger mit neuer Energie."),
                new InventoryItem(1,"Schild der Ausdauer", 1, 100, ItemRarity.SehrSelten, 6.0, 26,"Ein massiver Schild, der aus den härtesten Materialien der Erde gefertigt wurde.\n Der Schild der Ausdauer verleiht seinem Träger übermenschliche\n Standfestigkeit und Ausdauer. Jeder Schlag, den dieser Schild abwehrt, stärkt \n die Verteidigungskraft des Trägers, sodass er auch in den längsten und \n härtesten Schlachten standhalten kann."),
                new InventoryItem(2,"Großer Heiltrank", 5, 50, ItemRarity.Ungewoehnlich, 0.5, 100,"Eine große Phiole gefüllt mit einer leuchtend roten Flüssigkeit, die in den \n alten Tempeln der Heiler gebraut wurde. Der Große Heiltrank heilt \n nicht nur alle Wunden sofort, sondern stellt auch die verlorene Lebenskraft wieder her.\n Ein einzelner Tropfen dieser magischen Essenz kann ein ganzes Heer heilen."),
                new InventoryItem(3,"Gewöhnlicher Manatrank" ,10,10,ItemRarity.Gewoehnlich,10,100," Ein einfacher, aber wirkungsvoller Trank, der in einer unscheinbaren blauen \n Flasche geliefert wird. Der Gewöhnliche Manatrank stellt die \n magische Energie des Trägers wieder her und ermöglicht es ihm, seine Zauber weiterzuführen.\n Obwohl dieser Trank weit verbreitet ist, ist seine Wirksamkeit\n bei Abenteurern und Magiern gleichermaßen geschätzt."),
                new InventoryItem(4,"Hodors Langbogen der Macht", 1, 200, ItemRarity.Legendaer, 3.0, 54,"Ein majestätischer Langbogen, gefertigt aus dem Holz des ältesten Baums \n im magischen Wald von Eldoria. Der Bogen ist mit Runen verziert, \n die uralte Kräfte freisetzen. Hodors Langbogen der Macht schießt Pfeile mit \n unfehlbarer Präzision und durchschlägt die stärksten Rüstungen. \n Ein jeder Schuss aus diesem Bogen trägt die Macht der Natur und \n die Weisheit des legendären Bogenschützen Hodor in sich."),
                new InventoryItem(5,"Altes Holzschwert",1,2,ItemRarity.Gewoehnlich,2.0,20,"ein altes Schwert aus Holz gefertigt.")
            };
            
            //Erstelle ein neues Inventar und füge die oben erstellten Items dem Iventar zu
            myInventory = new Inventory(inventoryItems);

            //Zeige das (unsortierte) Inventar:
            DisplayInventory();

            //Das Programm am Beenden hindern
            Console.ReadLine();           
        }

        //Zeigt das Inventar oder ein Inventargegenstand an
        //Wenn InventorySlot -1 (Default Value) ist, 
        //wird das gesammte Inventar angezeigt. 
        //Ist InventarySlot größer als 0, wird das jew. Item exklusiv angezeigt.
        private static void DisplayInventory(int inventorySlot = -1)
        {
            //Prüfe ob das Inventar aktuell Sortiert ist.
            myInventory.DisplayInventory(inventorySlot);
            if (CurrentSortIndex > 0)
            {
                Console.WriteLine("Aktuelle Sortierung:"+ CurrentSortCreteria);
            }

            Console.WriteLine();
            //Menü Anzeigen
            if(inventorySlot == -1)
            {
                
                Console.WriteLine("Drücke:  s(Sortieren)    1-100(Anzeigen des jew. Item im Slot)  q(Inventar schließen)");
               
            }
            //Menü Anzeigen(einzelnes Item)
            else
            {
                Console.WriteLine("Drücke:  b(zurück zum Inventar)  d(zum fallenlassen des Items)   q(Inventar schließen)");
            }
            //Ausgewählten Menüpunkt verarbeiten
            var input = Console.ReadLine();
            HandleInput(input);
        }
        //Ausgewählten Menüpunkt verarbeiten
        private static void HandleInput(string? input)
        {
            if(input =="q")
            {
                Environment.Exit(0);
            }
            if (input == "b")
            {
                DisplayInventory();
            }
            //Prüfen ob es eine Zahl ist
            bool isNumeric = int.TryParse(input, out int selectedSlot);

            if (isNumeric)
            {
                DisplayInventory(selectedSlot);

            }
            if (input == "s")
            {
                SortInventory();
            }

            
        }
        //Das Inventar Sortieren (Rotierend)
        private static void SortInventory()
        {
            //Nach Namen sortieren
            if (CurrentSortIndex == 0)
            {
                CurrentSortIndex = 1;
                List<InventoryItem> currentInventory = myInventory.GetInventoryItems();
                CurrentSortCreteria = "Name";
                Sorter.BubbleSort(currentInventory, CurrentSortCreteria);
                DisplayInventory();

            }
            //Nach Menge sortieren
            else if (CurrentSortIndex == 1)
            {
                CurrentSortIndex = 2;
                List<InventoryItem> currentInventory = myInventory.GetInventoryItems();
                CurrentSortCreteria = "Menge";
                Sorter.BubbleSort(currentInventory, CurrentSortCreteria);
                DisplayInventory();
            }
            //Nach Wert sortieren
            else if (CurrentSortIndex == 2)
            {
                CurrentSortIndex = 3;
                List<InventoryItem> currentInventory = myInventory.GetInventoryItems();
                CurrentSortCreteria = "Wert";
                Sorter.BubbleSort(currentInventory, CurrentSortCreteria);
                DisplayInventory();
            }
            //Nach Seltenheit sortieren
            else if (CurrentSortIndex == 3)
            {
                CurrentSortIndex = 4;
                List<InventoryItem> currentInventory = myInventory.GetInventoryItems();
                CurrentSortCreteria = "Seltenheit";
                Sorter.BubbleSort(currentInventory, CurrentSortCreteria);
                DisplayInventory();
            }
            //Nach Gewicht sortieren
            else if (CurrentSortIndex == 4)
            {
                CurrentSortIndex = 5;
                List<InventoryItem> currentInventory = myInventory.GetInventoryItems();
                CurrentSortCreteria = "Gewicht";
                Sorter.BubbleSort(currentInventory, CurrentSortCreteria);
                DisplayInventory();
            }
            //Nach Haltbarkeit sortieren
            else if (CurrentSortIndex == 5)
            {
                CurrentSortIndex = 6;
                List<InventoryItem> currentInventory = myInventory.GetInventoryItems();
                CurrentSortCreteria = "Haltbarkeit";
                Sorter.BubbleSort(currentInventory, CurrentSortCreteria);
                DisplayInventory();
            }
            //Nach ID(Aufheben des Items) sortieren
            else if (CurrentSortIndex == 6)
            {
                CurrentSortIndex = 0;
                List<InventoryItem> currentInventory = myInventory.GetInventoryItems();
                CurrentSortCreteria = "ID";
                Sorter.BubbleSort(currentInventory, CurrentSortCreteria);
                DisplayInventory();
            }






        }
    }

  

   
}