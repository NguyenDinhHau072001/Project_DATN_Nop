using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDATN.Data.ViewModels
{
    public class ContactVM
    {
        [Key]
        public int Id { set; get; }
        public string? Name { set; get; }
        public string? Message { set; get; }
    }
}
