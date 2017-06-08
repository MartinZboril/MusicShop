using MusicShop.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.Database
{
    public class GoodsInformationDatabase
    { // SQLite connection
        private SQLiteAsyncConnection database;

        public GoodsInformationDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<GoodsInformation>().Wait();
        }

        // Query
        public Task<List<GoodsInformation>> GetItemsAsync()
        {
            return database.Table<GoodsInformation>().ToListAsync();
        }

        // Query using SQL query string
        public Task<List<GoodsInformation>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<GoodsInformation>("SELECT * FROM [GoodsInformation]");
        }
        // Query using LINQ
        public Task<GoodsInformation> GetItemAsync(int id)
        {
            return database.Table<GoodsInformation>().Where(i => i.GoodsInformationID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(GoodsInformation item)
        {
            if (item.GoodsInformationID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(GoodsInformation item)
        {
            return database.DeleteAsync(item);
        }
    }
}


