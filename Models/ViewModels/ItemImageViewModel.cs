namespace PersonalProject.Models.ViewModels
{
    public class ItemImageViewModel
    {
        public Item Item { get; set; } = new Item();
        public List<Item> Items { get; set; } = new List<Item>();
        public List<ItemImages> ItemImages { get; set; } = new List<ItemImages>();

    }
}
