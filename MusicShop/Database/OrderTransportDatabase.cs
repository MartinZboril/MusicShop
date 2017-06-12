using MusicShop.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.Database
{
    public class OrderTransportDatabase
    {// SQLite connection
        private SQLiteAsyncConnection database;

        public OrderTransportDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<OrderTransport>().Wait();
        }

        // Query
        public Task<List<OrderTransport>> GetItemsAsync()
        {
            return database.Table<OrderTransport>().ToListAsync();
        }

        // Query using SQL query string
        public Task<List<OrderTransport>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<OrderTransport>("SELECT * FROM [OrderTransport]");
        }

        // Query using LINQ
        public Task<OrderTransport> GetItemAsync(int id)
        {
            return database.Table<OrderTransport>().Where(i => i.TransportID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(OrderTransport item)
        {
            if (item.TransportID != 0)
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