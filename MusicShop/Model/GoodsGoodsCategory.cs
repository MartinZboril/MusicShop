using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.Model
{
    public class GoodsGoodsCategory
    {
        [PrimaryKey, AutoIncrement]
        public int GoodsGoodsCategoryID { get; set; }
        public int GoodsID { get; set; }
        public int GoodsCategoryID { get; set; }
    }
}
