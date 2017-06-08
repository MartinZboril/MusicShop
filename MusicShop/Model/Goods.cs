using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.Model
{
    public class Goods
    {
        [PrimaryKey, AutoIncrement]
        public int GoodsID { get; set; }
        public int GoodsCategoryID { get; set; }
        public int GoodsImageID { get; set; }
        public int GoodsInformationID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
    }
}
