using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Model;

namespace ProductsAPI.Repository
{
    public interface IProductRepository
    {
        void DeleteProduct(int productId);
        Task<Product> GetAsyncProductByID(int productId);
        Task<List<Product>> GetAsyncProducts(IEnumerable<int> ids);
        Task<List<Product>> GetAsyncProductsItemsForPage(int pageSize, int pageIndex);
        Task<long> GetAsyncProductCount();
        Product GetProductByID(int productId);
        IEnumerable<Product> GetProducts();
        void InsertProduct(Product product);
        void Save();
        void UpdateProduct(Product product);
        Task SaveChangesAsync();
    }
}