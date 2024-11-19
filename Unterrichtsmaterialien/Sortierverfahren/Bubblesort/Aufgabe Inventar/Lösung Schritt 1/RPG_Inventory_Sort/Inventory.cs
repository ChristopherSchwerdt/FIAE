using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;


namespace RPG_Inventory_Sort
{
    internal class Inventory
    {

        private List<InventoryItem> _inventoryItems;

        public Inventory(List<InventoryItem> items) {
            _inventoryItems = items;
        }
        public List<InventoryItem> GetInventoryItems()
        {
            return _inventoryItems;
        }
        //Das Inventar rendern
        public void DisplayInventory(int showSlot =-1)
        {
            Console.Clear();
            bool showOnlySlot = false;
            if (showSlot >0)
            {
                showOnlySlot = true;
            }
            Console.WriteLine("----------------------------------------------------------------------------------------------------");
            Console.WriteLine("                                          Dein Inventar                                       ");
            Console.WriteLine("----------------------------------------------------------------------------------------------------");
            Console.WriteLine();

            Console.WriteLine(String.Format("{0,-5}  {1,-30} {2,-10} {3,-10} {4,-15} {5,-10} {6,-12} ","Slot","Name","Menge","Wert","Seltenheit","Gewicht","Haltbarkeit"));
            Console.WriteLine("----------------------------------------------------------------------------------------------------");
            int i = 1;
            foreach (var item in _inventoryItems)
            {
                if (!showOnlySlot) {
                    // Console.WriteLine(String.Format("|{0,-5} {1,-30} {2,-10} {3,-10} {4,-15} {5,-10} {6,-12}|", i, item.Name, item.Quantity, item.Value + "G", item.Rarity.GetDisplayName(), item.Weight + "kg", item.Durability + "%"));
                    string rarityDisplayName = item.Rarity.GetDisplayName();

                    Console.Write("|{0,-5} {1,-30} {2,-10} {3,-10} ", i, item.Name, item.Quantity, item.Value + "G");


                    // Setze die Farbe für die Seltenheit:
                    
                    if (item.Rarity == ItemRarity.Gewoehnlich)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }
                    else if (item.Rarity == ItemRarity.Ungewoehnlich)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (item.Rarity == ItemRarity.Selten)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if (item.Rarity == ItemRarity.SehrSelten)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    else if (item.Rarity == ItemRarity.Legendaer)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    }
                    else if (item.Rarity == ItemRarity.Artefakt)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.Write($"{rarityDisplayName,-15}");
                    Console.ResetColor();
                    Console.Write(" ");

                    Console.WriteLine("{0,-10} {1,-12}|", item.Weight + "kg", item.Durability + "%");
                    Console.WriteLine("----------------------------------------------------------------------------------------------------");
                }
                else
                {
                    if(i == showSlot)
                    {
                        Console.WriteLine(String.Format("|{0,-5} {1,-30} {2,-10} {3,-10} {4,-15} {5,-10} {6,-12}|", i, item.Name, item.Quantity, item.Value + "G", item.Rarity.GetDisplayName(), item.Weight + "kg", item.Durability + "%"));
                        Console.WriteLine("----------------------------------------------------------------------------------------------------");
                        Console.WriteLine("Beschreibung:");
                        Console.WriteLine(item.FullDescription);
                    }
                }
                i++;
            }           
        }

    }

    //Die Seltenheitsgrade eines Objektes
    //Manche Seltenheitsgrade haben einen Umlaut oder eine Leerstelle.
    //Dafür wird dann der "Anzeigename" ensprechend genutzt.
    public enum ItemRarity
    {
        [Display(Name = "Gewöhnlich")]
        Gewoehnlich,
        [Display(Name = "Ungewöhnlich")]
        Ungewoehnlich,
        Selten ,
        [Display(Name = "Sehr selten")]
        SehrSelten ,
        [Display(Name = "Legendär")]
        Legendaer,
        Artefakt
    }

    public static class EnumExtensions
    {
        //Ja, anscheinend ist es nötig eine extra Funktion zu schreiben um auf den "Anzeigenamen" von einem Enum zurückzugreifen. (Augenroll-Smiley)
        //Danke an Chat-GPT an der stelle...
        public static string GetDisplayName(this Enum enumValue)
        {
            var enumType = enumValue.GetType();
            var memberInfo = enumType.GetMember(enumValue.ToString());

            if (memberInfo != null && memberInfo.Length > 0)
            {
                var attrs = memberInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false);
                if (attrs != null && attrs.Length > 0)
                {
                    return ((DisplayAttribute)attrs[0]).Name;
                }
            }
            return enumValue.ToString();
        }
    }
}
