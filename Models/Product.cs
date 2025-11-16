using System.ComponentModel.DataAnnotations;

namespace StockManagementSystem.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public required string ProductCode { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int SoldQuantity { get; set; }
        public int Quantity { get; set; }
        public string? Image { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}