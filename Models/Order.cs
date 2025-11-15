namespace StockManagementSystem.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? CustomerName { get; set; }
        public string? SalesPerson { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public Product? Product { get; set; }
    }

    public class SalesRanking
    {
        public string? SalesPerson { get; set; }
        public decimal TotalSales { get; set; }
        public int TotalOrders { get; set; }
        public int Rank { get; set; }
    }
}