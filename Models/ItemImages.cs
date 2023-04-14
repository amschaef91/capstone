using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalProject.Models
{
    public class ItemImages
    {
        [Key, ForeignKey("Items")]
        public int ItemID { get; set; }
        public Item Item { get; set; } = new Item();
        public string ImgPath { get; set; } = string.Empty;

    }
}
