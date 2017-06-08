using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicShop.Model;

namespace MusicShop
{
    public class GoodsDatabase
    {
        // SQLite connection
        private SQLiteAsyncConnection database;

        public GoodsDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Goods>().Wait();
        }

        // Query
        public Task<List<Goods>> GetItemsAsync()
        {
            return database.Table<Goods>().ToListAsync();
        }

        // Query using SQL query string
        public Task<List<Goods>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<Goods>("SELECT * FROM [Goods]");
        }

        public Task<List<Goods>> GetGoodsByName()
        {
            return database.QueryAsync<Goods>("SELECT * FROM [Goods] ORDER BY Name");
        }

        public Task<List<Goods>> GetGoodsByHighestPrice()
        {
            return database.QueryAsync<Goods>("SELECT * FROM [Goods] ORDER BY Price DESC");
        }

        public Task<List<Goods>> GetGoodsByLowestPrice()
        {
            return database.QueryAsync<Goods>("SELECT * FROM [Goods] ORDER BY Price");
        }

        public Task<List<Goods>> GetSearchWord(string word)
        {
            return database.QueryAsync<Goods>("SELECT * FROM [Goods] WHERE Name LIKE '"+word+'%'+"'");
        }

        // Query using LINQ
        public Task<Goods> GetItemAsync(string name)
        {
            return database.Table<Goods>().Where(i => i.Name == name).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Goods item)
        {
            if (item.GoodsID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(Goods item)
        {
            return database.DeleteAsync(item);
        }
    }
}
