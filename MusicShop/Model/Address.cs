using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.Model
{
    public class Address
    {
        [PrimaryKey, AutoIncrement]
        public int AddressID { get; set; }
        public int CustomerID { get; set; }
        public string Street { get; set; }
        public int StreetNumber { get; set; }
        public string Town { get; set; }
        public int PostNumber { get; set; }
        public string State { get; set; }
    }
}
