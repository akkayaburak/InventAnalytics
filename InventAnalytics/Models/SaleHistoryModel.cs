using System;

namespace InventAnalytics.Models
{
    public class SaleHistoryModel
    {
        public string ProductName { get; set; }
        public string StoreName { get; set; }
        public DateTime Date { get; set; }
        public int SalesQuantity { get; set; }
        public int Stock { get; set; }
    }
}
