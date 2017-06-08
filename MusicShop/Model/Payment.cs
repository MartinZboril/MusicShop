using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.Model
{
    class Payment
    {
        [PrimaryKey, AutoIncrement]
        public int PaymentID {get; set;}
        public string PaymentType { get; set; }
    }
}
