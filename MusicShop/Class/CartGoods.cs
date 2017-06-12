using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.Class
{
    public class CartGoods
    {
        public int GoodsID { get; set; }
        public int ImageID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int GoodsQauntity { get; set; }
        public int TotalPrice { get; set; }
    }
}
