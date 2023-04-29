using Microsoft.EntityFrameworkCore;
using ProjectDATN.Data.Entities;

namespace ProjectDATN.Data.EF
{
    public interface IApplicationDBContext
    {
        DbSet<Brand> Brands { get; set; }
        DbSet<Cart> Carts { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Contact> Contacts { get; set; }
        DbSet<OrderDetail> OrderDetails { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<Product> Products { get; set; }
    }
}