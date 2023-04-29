using ProjectDATN.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDATN.Data.ViewModels
{
    public class PrmotionVM
    {
        public PrmotionVM()
        {
        }

        public PrmotionVM(int id, int proId, string? proName, string? proImage, KM kM, DateTime created, DateTime finish)
        {
            Id = id;
            ProId = proId;
            ProName = proName;
            ProImage = proImage;
            KM = kM;
            Created = created;
            Finish = finish;
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public int ProId { get; set; }
        public string? ProName { get; set; }
        public string? ProImage { get; set; }
        public decimal? PurChasePrice { get; set; }
        public decimal? Price { get; set; }
        [Required]
        public KM KM { get; set; }  
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public DateTime Finish { get; set; }
    }
}
