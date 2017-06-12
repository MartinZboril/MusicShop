using MusicShop.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.Database
{
    public class ContactInformationDatabase
    {// SQLite connection
        private SQLiteAsyncConnection database;

        public ContactInformationDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<ContactInformation>().Wait();
        }

        // Query
        public Task<List<ContactInformation>> GetItemsAsync()
        {
            return database.Table<ContactInformation>().ToListAsync();
        }

        // Query using SQL query string
        public Task<List<ContactInformation>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<ContactInformation>("SELECT * FROM [ContactInformation]");
        }

        // Query using LINQ
        public Task<ContactInformation> GetItemAsync(int id)
        {
            return database.Table<ContactInformation>().Where(i => i.ContactInformationID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(ContactInformation item)
        {
            if (item.ContactInformationID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(ContactInformation item)
        {
            return database.DeleteAsync(item);
        }
    }
}
