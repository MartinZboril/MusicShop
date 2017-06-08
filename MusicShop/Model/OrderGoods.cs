using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicShop.Model
{
    public class OrderGoods
    {
        [PrimaryKey, AutoIncrement]
        public int OrderGoodsID { get; set; }
        public int OrderID { get; set; }
        public int GoodsID { get; set; }
    }
}