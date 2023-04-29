
using ProjectDATN.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDATN.Data.Entities
{
    public class Category
    {
        [Key]
        public int Id { set; get; }
        public string CateName { set; get; }
        public DateTime Created { set; get; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
        public string? Slug { set; get; }

        public ICollection<Product>? Products { set; get; }
    }
}
