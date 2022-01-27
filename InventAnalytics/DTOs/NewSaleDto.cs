using System;

namespace InventAnalytics.DTOs
{
    public class NewSaleDto
    {
        public string DateStr { get { return Date.ToString("yyyy-MM-dd"); } }
        public int ProductId { get; set; }
        public int StoreId { get; set; }
        public DateTime Date { get; set; }
        public int SaleQuantity { get; set; }
        public int Stock { get; set; }
    }
}
