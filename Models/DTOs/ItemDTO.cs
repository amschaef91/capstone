namespace PersonalProject.Models.DTOs
{
    public class ItemDTO
    {
        public int ItemID { get; set; } 
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }  
        public int Stock { get; set; }

        public ItemDTO() { }

        public ItemDTO(Item item)
        {
            ItemID = item.ItemID;
            Name = item.Name;
            Description = item.Description;
            Price = item.Price;
            Stock = item.Stock;
        }
    }
}
