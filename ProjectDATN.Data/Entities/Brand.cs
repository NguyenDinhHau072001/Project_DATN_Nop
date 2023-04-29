using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDATN.Data.Entities
{
    public class Brand
    {
        public int Id { set; get; }
        public string Name { set; get; }

        public DateTime Created { get; set; } = DateTime.Now;

        public ICollection<Product>? Products { set; get; }
    }
}
