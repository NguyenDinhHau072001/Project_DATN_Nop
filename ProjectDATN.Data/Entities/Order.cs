using Microsoft.AspNetCore.Mvc;
using ProjectDATN.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDATN.Data.Entities
{
    public class Order
    {
        [Key]
        public int Id { set; get; }
        [BindProperty, DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime OrderDate { set; get; } = DateTime.Now;
        public string UserId { set; get; }
        public string? ShipperId { set; get; }
        public string? UserName { set; get; }
        public string Address { set; get; }
        public string PhoneNumber { set; get; }
        public decimal TotalPrice { set; get; }
        public PaymentStatus Payment {  set; get; }
        public bool IsPay { get; set; } = false;
        public OrderStatus Status { set; get; }
        public List<OrderDetail>? OrderDetails { get; set; }
        public AppUser AppUser { get; set; }
    }
}
