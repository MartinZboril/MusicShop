using MusicShop.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.Database
{
    public class CustomerDatabase
    {// SQLite connection
        private SQLiteAsyncConnection database;

        public CustomerDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Customer>().Wait();
        }

        // Query
        public Task<List<Customer>> GetItemsAsync()
        {
            return database.Table<Customer>().ToListAsync();
        }

        // Query using SQL query string
        public Task<List<Customer>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<Customer>("SELECT * FROM [Customer]");
        }

        // Query using LINQ
        public Task<Customer> GetItemAsync(int id)
        {
            return database.Table<Customer>().Where(i => i.CustomerID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Customer item)
        {
            if (item.CustomerID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(Customer item)
        {
            return database.DeleteAsync(item);
        }
    }
}