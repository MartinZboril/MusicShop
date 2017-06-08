using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.Model
{
    public class GoodsInformation
    {
        [PrimaryKey, AutoIncrement]
        public int GoodsInformationID { get; set; }
        public int YearOfRealising { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }
}
