using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.Model
{
    public class GoodsCategory
    {
        [PrimaryKey, AutoIncrement]
        public int GoodsCategoryID { get; set; }
        public string Category { get; set; }
    }
}
