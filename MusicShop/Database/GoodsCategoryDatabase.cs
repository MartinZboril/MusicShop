using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicShop.Model;

namespace MusicShop.Database
{
    public class GoodsCategoryDatabase
    {
        // SQLite connection
        private SQLiteAsyncConnection database;

        public GoodsCategoryDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<GoodsCategory>().Wait();
        }

        // Query
        public Task<List<GoodsCategory>> GetItemsAsync()
        {
            return database.Table<GoodsCategory>().ToListAsync();
        }

        // Query using SQL query string
        public Task<List<GoodsCategory>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<GoodsCategory>("SELECT * FROM [GoodsCategory] ORDER BY Category");
        }

        // Query using LINQ
        public Task<GoodsCategory> GetCategoryByName(string category)
        {
            return database.Table<GoodsCategory>().Where(i => i.Category == category).FirstOrDefaultAsync();
        }


        // Query using LINQ
        public Task<GoodsCategory> GetItemAsync(int id)
        {
            return database.Table<GoodsCategory>().Where(i => i.GoodsCategoryID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(GoodsCategory item)
        {
            if (item.GoodsCategoryID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(GoodsCategory item)
        {
            return database.DeleteAsync(item);
        }
    }
}

