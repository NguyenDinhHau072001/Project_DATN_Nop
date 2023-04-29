using ProjectDATN.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDATN.Data.ViewModels
{
    public class ProductVM
    {
        [Key]
        public int Id { get; set; }
        public string ProName { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public int SaleQuatity { get; set; }
        public decimal Price { get; set; }
        public decimal PerchasePrice { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public string? Image { get; set; }
        public int CateID { get; set; }
        public string? CateName { get; set; }

        public int BandID { get; set; }
        public string? BrandName { get; set; }

        public List<OrderDetail>? OrderDetails { get; set; }

        public List<Cart>? Carts { get; set; }

        public ProductVM()
        {

        }
        public ProductVM(int id, string proName, string? description, int quantity, decimal price, decimal perchasePrice, DateTime created, string? image, int cateID, string cateName, int bandID, string brandName)
        {
            Id = id;
            ProName = proName;
            Description = description;
            Quantity = quantity;
            Price = price;
            PerchasePrice = perchasePrice;
            Created = created;
            Image = image;
            CateID = cateID;
            CateName = cateName;
            BandID = bandID;
            BrandName = brandName;
          
        }
    }
}
