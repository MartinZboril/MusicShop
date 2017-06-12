using MusicShop.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.Database
{
    public class OrderDatabase
    {// SQLite connection
        private SQLiteAsyncConnection database;

        public OrderDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Order>().Wait();
        }

        // Query
        public Task<List<Order>> GetItemsAsync()
        {
            return database.Table<Order>().ToListAsync();
        }

        // Query using SQL query string
        public Task<List<Order>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<Order>("SELECT * FROM [Order]  ORDER BY OrderID DESC");
        }

        // Query using LINQ
        public Task<Order> GetItemAsync(int id)
        {
            return database.Table<Order>().Where(i => i.OrderNumber == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Order item)
        {
            if (item.OrderID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(Order item)
        {
            return database.DeleteAsync(item);
        }
    }
}