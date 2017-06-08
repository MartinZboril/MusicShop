using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MusicShop.Model
{
    public class Customer
    {
        [PrimaryKey, AutoIncrement]
        public int CustomerID { get; set; }
        public int AddressID { get; set; }
        public int ContactInformationID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
