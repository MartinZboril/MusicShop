using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.Model
{
    public class OrderTransport
    {
        [PrimaryKey, AutoIncrement]
        public int TransportID { get; set; }
        public string TypeOfTransport { get; set; }
        public int Price { get; set; }
    }
}
