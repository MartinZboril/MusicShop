using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.Model
{
    public class GoodsImage
    {
        [PrimaryKey, AutoIncrement]
        public int ImageID { get; set; }
        public string ImageName { get; set; }
    }
}
