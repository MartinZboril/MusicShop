using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.Model
{
    public class Order
    {
        [PrimaryKey, AutoIncrement]
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public int OrderInformationID { get; set; }
        public int TransportID { get; set; }
        public string OrderName { get; set; }
        public string OrderPrice{ get; set; }
    }
}
