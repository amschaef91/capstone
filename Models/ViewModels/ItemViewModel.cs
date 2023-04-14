using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PersonalProject.Models;

namespace PersonalProject.Models.ViewModels
{
    public class ItemViewModel
    {
        public int ItemID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public ItemImages? ItemImages { get; set; }
        public List<IFormFile>? Images { get; set; }
    }
}


