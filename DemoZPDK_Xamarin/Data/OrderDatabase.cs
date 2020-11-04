using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using DemoZPDK_Xamarin.Models;
using DemoZPDK_Xamarin;
using DemoZPDK_Xaramin_V2.Helper;

namespace DemoZPDK_Xamarin.Data
{
    public class OrderDatabase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public OrderDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Order).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Order)).ConfigureAwait(false);
                }
                initialized = true;
            }
        }

        public Task<List<Order>> GetItemsAsync()
        {
            return Database.Table<Order>().OrderByDescending(x => x.ID).ToListAsync();
        }

        public Task<List<Order>> GetItemsDoneAsync()
        {
            return Database.QueryAsync<Order>("SELECT * FROM [Order] WHERE [Status] = 1");
        }

        public Task<Order> GetItemAsync(int id)
        {
            return Database.Table<Order>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<Order> GetItemAsyncByAppTransId(string appTransId)
        {
            return Database.Table<Order>().Where(i => i.AppTransId == appTransId).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Order order)
        {

            if (order.ID != 0)
            {
                return Database.UpdateAsync(order);
            }
            else
            {
                return Database.InsertAsync(order);
            }
        }

        public Task<int> DeleteItemAsync(Order order)
        {
            return Database.DeleteAsync(order);
        }
    }
}
