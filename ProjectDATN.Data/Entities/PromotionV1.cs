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
        public DateTime Created { get; set; }
        [Required]
        public DateTime Finish { get; set; }
    }

}

