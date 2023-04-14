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
        public int ID { get; set; }
        [ForeignKey("Items")]
        public int ItemID { get; set; }
        public Item? Item { get; set; }
        public string ImgPath { get; set; } = string.Empty;
        public bool IsMain { get; set; }

        public ItemImages() { }

        public ItemImages(int itemID, string imgPath)
        {
            ItemID = itemID;
            ImgPath = imgPath;
        }
    }
}
