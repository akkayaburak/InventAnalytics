using InventAnalytics.Models;
using InventAnalytics.Persistence;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace InventAnalytics.Operations
{
    public class StoreOperations
    {
        private readonly DataContext _context;

        public StoreOperations(DataContext context)
        {
            _context = context;
        }

        public async Task<int> Profit(int storeId)
        {
            var statement = @$"SELECT TOP 1 SUM(SalesPrice - Cost) AS C FROM InventorySales ISA
                               INNER JOIN Products P ON P.Id = ISA.ProductId
                               WHERE StoreId = {storeId} AND SalesQuantity > 0
                               GROUP BY ProductId
                               ORDER BY C DESC;";

            var con = _context.Open();
            SqlCommand command = new(statement, con);

            int profit = (int)await command.ExecuteScalarAsync();

            return profit;
        }

        public async Task<StoreModel> MostProfitableStore()
        {
            var statement = @$"SELECT * FROM Stores S WHERE S.Id = (SELECT TOP 1 StoreId FROM InventorySales ISA
                               INNER JOIN Products P ON P.Id = ISA.ProductId
                               GROUP BY StoreId
                               ORDER BY SUM(SalesPrice - Cost) DESC);";

            var con = _context.Open();
            SqlCommand command = new(statement, con);

            StoreModel storeModel = new();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                storeModel.Id = reader.GetInt32(0);
                storeModel.StoreName = reader.GetString(1);
            }
            
            return storeModel;
        }
    }
}
