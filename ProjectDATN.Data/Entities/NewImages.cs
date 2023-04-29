using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDATN.Data.Entities
{
    public class NewImages
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NewId { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
