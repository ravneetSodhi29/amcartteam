using Microsoft.EntityFrameworkCore;
using ProductsAPI.Model;

namespace ProductsAPI.DbContexts
{
    public interface IProductContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Product> Products { get; set; }
    }
}