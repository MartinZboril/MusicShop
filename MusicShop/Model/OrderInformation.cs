using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.Model
{
    class OrderInformation
    {
        [PrimaryKey, AutoIncrement]
        public int OrderInformationID { get; set; }
        public int OrderNumber { get; set; }
        public string OrderDescription { get; set; }
    }
}
