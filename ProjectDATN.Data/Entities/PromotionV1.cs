using Microsoft.AspNetCore.Mvc;
using ProjectDATN.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDATN.Data.Entities
{
    public class PromotionV1
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProId { get; set; }
        [Required]
        public KM Promo { get; set; }
        
        [Required]
        [BindProperty, DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Created { get; set; }
        
        [Required]
        [BindProperty, DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Finish { get; set; }
    }

}

