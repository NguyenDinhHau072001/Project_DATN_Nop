using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ProjectDATN.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDATN.Data.ViewModels
{
    public class OrderDetailVM
    {
        public int Id { get; set; }
        public int OrderId { set; get; }
        public int? ProductId { set; get; }
        public string? ProductName { set; get; }
        public string? Image { get; set; }
        public int Quantity { set; get; }
        public decimal Price { set; get; }
        public DateTime CreatedDate { set; get; }

        public Order? Order { get; set; }

        public Product? Product { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
