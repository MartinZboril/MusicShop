using MusicShop.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.Database
{
    public class OrderGoodsDatabase
    {// SQLite connection
        private SQLiteAsyncConnection database;

        public OrderGoodsDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<OrderGoods>().Wait();
        }

        // Query
        public Task<List<OrderGoods>> GetItemsAsync()
        {
            return database.Table<OrderGoods>().ToListAsync();
        }

        // Query using SQL query string
        public Task<List<OrderGoods>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<OrderGoods>("SELECT * FROM [OrderGoods]");
        }

        // Query using SQL query string
        public Task<List<OrderGoods>> GetSpecificOrderGoods(int number)
        {
            return database.QueryAsync<OrderGoods>("SELECT * FROM [OrderGoods]  WHERE OrderID = '" + '%' + number + '%' + "'");
        }

        // Query using LINQ
        public Task<OrderGoods> GetItemAsync(int id)
        {
            return database.Table<OrderGoods>().Where(i => i.OrderID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(OrderGoods item)
        {
            if (item.OrderGoodsID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(OrderGoods item)
        {
            return database.DeleteAsync(item);
        }
    }
}