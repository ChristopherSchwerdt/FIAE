namespace RPG_Inventory_Sort
{
   internal static class Sorter
    {

        //Der Bubblesort Algorithmus
        public static void BubbleSort(List<InventoryItem> inventory, string criteria)
        {
            
        }
        //Vergleich zweier Items nach einem gewählten Kriterium
        private static int CompareItems(InventoryItem item1, InventoryItem item2, string criteria)
        {
            switch (criteria)
            {
                case "Name":
                    return string.Compare(item1.Name, item2.Name);
               //...
               // Hier die Kreterien ergänzen...
               //...
                case "ID":
                    return item1.GetID().CompareTo(item2.GetID());
                default:
                    throw new ArgumentException("Unbekanntes Sortierkriterium: " + criteria);
            }
        }
    }
}
