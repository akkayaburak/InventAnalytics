using InventAnalytics.DTOs;
using InventAnalytics.Models;
using InventAnalytics.Persistence;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace InventAnalytics.Operations
{
    public class SaleOperations
    {
        private readonly DataContext _context;
        public SaleOperations(DataContext context)
        {
            _context = context;
        }
        public async Task<List<SaleHistoryModel>> GetSaleHistoryAsync()
        {
            var statement = @$"SELECT P.ProductName, S.StoreName, ISA.Date, ISA.SalesQuantity, ISA.Stock FROM InventorySales ISA 
                               INNER JOIN Products P ON ISA.ProductId = P.Id
                               INNER JOIN Stores S ON S.Id = ISA.StoreId";
            var con = _context.Open();
            SqlCommand command = new(statement, con);

            List<SaleHistoryModel> saleHistories = new();
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (reader.Read())
                {
                    saleHistories.Add(new SaleHistoryModel
                    {
                        ProductName = reader.GetString(0),
                        StoreName = reader.GetString(1),
                        Date = reader.GetDateTime(2).Date,
                        SalesQuantity = reader.GetInt32(3),
                        Stock = reader.GetInt32(4)
                    });
                }
            }
            return saleHistories;
        }

        public async Task Add(NewSaleDto newSaleDto)
        {
            var statement = @$"INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity, Stock) 
                               VALUES ({newSaleDto.ProductId},{newSaleDto.StoreId},'{newSaleDto.DateStr}',{newSaleDto.SaleQuantity},{newSaleDto.Stock})";
            var con = _context.Open();

            SqlCommand command = new(statement, con);

            await command.ExecuteNonQueryAsync();
        }
        
        public async Task Delete(int id)
        {
            var statement = @$"DELETE FROM InventorySales ISA WHERE ISA.Id = {id}";

            var con = _context.Open();

            SqlCommand command = new(statement, con);

            await command.ExecuteNonQueryAsync();
        }
    }
}
