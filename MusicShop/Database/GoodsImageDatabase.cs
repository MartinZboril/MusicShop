using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MusicShop.Model;
using System.Threading.Tasks;

namespace MusicShop.Database
{
    public class GoodsImageDatabase
    {// SQLite connection
        private SQLiteAsyncConnection database;

        public GoodsImageDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<GoodsImage>().Wait();
        }

        // Query
        public Task<List<GoodsImage>> GetItemsAsync()
        {
            return database.Table<GoodsImage>().ToListAsync();
        }

        // Query using SQL query string
        public Task<List<GoodsImage>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<GoodsImage>("SELECT * FROM [GoodsImage]");
        }

        // Query using LINQ
        public Task<GoodsImage> GetItemAsync(int id)
        {
            return database.Table<GoodsImage>().Where(i => i.ImageID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(GoodsImage item)
        {
            if (item.ImageID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(GoodsImage item)
        {
            return database.DeleteAsync(item);
        }
    }
}