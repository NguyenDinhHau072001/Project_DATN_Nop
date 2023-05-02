using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDATN.Data.Entities
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { set; get; }
        public int ProductId { set; get; }
        public int Quantity { set; get; }
        public decimal Price { set; get; }
        public DateTime CreatedDate { set; get; }

        public Order? Order { get; set; }

        public Product? Product { get; set; }

    }
}
