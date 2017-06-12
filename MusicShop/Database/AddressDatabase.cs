using MusicShop.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.Database
{
    public class AddressDatabase
    {// SQLite connection
        private SQLiteAsyncConnection database;

        public AddressDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Address>().Wait();
        }

        // Query
        public Task<List<Address>> GetItemsAsync()
        {
            return database.Table<Address>().ToListAsync();
        }

        // Query using SQL query string
        public Task<List<Address>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<Address>("SELECT * FROM [Address]");
        }

        // Query using LINQ
        public Task<Address> GetItemAsync(int id)
        {
            return database.Table<Address>().Where(i => i.AddressID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Address item)
        {
            if (item.AddressID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(Address item)
        {
            return database.DeleteAsync(item);
        }
    }
}