using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.Model
{
    public class ContactInformation
    {
        [PrimaryKey, AutoIncrement]
        public int ContactInformationID { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
    }
}
