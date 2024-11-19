namespace RPG_Inventory_Sort
{
   internal static class Sorter
    {

        //Der Bubblesort Algorithmus
        public static void BubbleSort(List<InventoryItem> inventory, string criteria)
        {
            //Größe des Inventars
            int n = inventory.Count;
            //Boolischer Wert ob in dieser iteration getauscht worden ist
            bool swapped;
            //Erste schleife: Für jedes Item im Inventar wird einmal interiert
            for (int i = 0; i < n - 1; i++)
            {
                swapped = false;
                //zweite Schleife: sog. "Bubblephase" es werden jeweils 2 Items paarweise miteinander verglichen
                for (int j = 0; j < n - i - 1; j++)
                {
                    //Vergleich der beiden Items nach dem gewählten Kriterium
                    if (CompareItems(inventory[j], inventory[j + 1], criteria) > 0)
                    {
                        // Tausche inventory[j] und inventory[j+1]
                        InventoryItem temp = inventory[j];
                        inventory[j] = inventory[j + 1];
                        inventory[j + 1] = temp;
                        swapped = true;
                    }
                }

                // Wenn es in der inneren Schleife(Bubblephase) keinen tauschvorgang gab, ist die Liste(das Inventar)
                // sortiert und der algorithmus kann beendet werden.
                if (!swapped)
                    break;
            }
        }
        //Vergleich zweier Items nach einem gewählten Kriterium
        private static int CompareItems(InventoryItem item1, InventoryItem item2, string criteria)
        {
            switch (criteria)
            {
                case "Name":
                    return string.Compare(item1.Name, item2.Name);
                case "Menge":
                    return item1.Quantity.CompareTo(item2.Quantity);
                case "Wert":
                    return item1.Value.CompareTo(item2.Value);
                case "Seltenheit":
                    return item1.Rarity.CompareTo(item2.Rarity);
                case "Gewicht":
                    return item1.Weight.CompareTo(item2.Weight);
                case "Haltbarkeit":
                    return item1.Durability.CompareTo(item2.Durability);
                case "ID":
                    return item1.GetID().CompareTo(item2.GetID());
                default:
                    throw new ArgumentException("Unbekanntes Sortierkriterium: " + criteria);
            }
        }
    }
}
