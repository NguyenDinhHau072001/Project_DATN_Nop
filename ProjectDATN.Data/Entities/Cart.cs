using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDATN.Data.Entities
{
    public class Cart
    {
        [Key]
        public int Id { set; get; }
        public int ProductId { set; get; }
        public int Quantity { set; get; }
        public decimal Price { set; get; }

        public Guid UserId { get; set; }

        public Product Product { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateCreated { get; set; }
        public AppUser AppUser { get; set; }
    }
}
