using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProjectDATN.Data.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string ProName { get; set; }
        public string? Description { get; set; }
        public int Quality { get; set; }
        public int SaleQuatity { get; set; }
        public decimal Price { get; set; }
        public decimal PerchasePrice { get; set; }
        public DateTime Created { get; set; }
        public string? Image { get; set; }
        public int CateID { get; set; }
        public int BandID { get; set; }

        public List<OrderDetail>? OrderDetails { get; set; }

        public List<Cart>? Carts { get; set; }

        [NotMapped]

        public IFormFile? ImageFile { get; set; }
    }
}
