using ProjectDATN.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDATN.Data.ViewModels
{
    public class MuaHangVM
    {
        public string UserId { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập họ và tên")]
        public string FullName { get; set; }

        public string Email { get; set; }
        [Required(ErrorMessage ="Vui long nhập số điện thoại")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập địa chỉ nhận hàng")]
        public string Address { get; set; }
        public string Tinh { get; set; }
        public string? Huyen { get; set; }
        public string PhuongXa { get; set; }
        public PaymentStatus Payment { get; set; }
        public string? Note { get; set; }
    }
}
