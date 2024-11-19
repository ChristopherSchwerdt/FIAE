namespace RPG_Inventory_Sort
{

    //Die Klasse für ein Inventaritem
    internal class InventoryItem
    {
        private int ID;
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Value { get; set; }
        public ItemRarity Rarity { get; set; }
        public double Weight { get; set; }
        public int Durability { get; set; }
        public string FullDescription { get; set; }
       
        //Konstruktor
        public InventoryItem(int id,string name, int quantity, int value, ItemRarity itemRarity, double weight, int durability, string fullDescription)
        {
            Name = name;
            Quantity = quantity;
            Value = value;
            Rarity = itemRarity;
            Weight = weight;
            Durability = durability;
            FullDescription = fullDescription;
            ID = id;
        }
        public int GetID()
        {
            return ID;
        }
    }

}
