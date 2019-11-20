using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.DbContexts;
using ProductsAPI.Model;

namespace ProductsAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _dbContext;

        public ProductRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void DeleteProduct(int productId)
        {
            var product = _dbContext.Products.Find(productId);
            _dbContext.Products.Remove(product);
            Save();
        }

        public Product GetProductByID(int productId)
        {
            return _dbContext.Products.Find(productId);
        }

        public async Task<Product> GetAsyncProductByID(int productId)
        {
            return await _dbContext.Products.SingleOrDefaultAsync(pi => pi.Id == productId);
        }

        public async Task<long> GetAsyncProductCount()
        {
            return await _dbContext.Products.LongCountAsync();
        }

        public IEnumerable<Product> GetProducts()
        {
            return _dbContext.Products.ToList();
        }
        
        public void InsertProduct(Product product)
        {
            _dbContext.Add(product);
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            _dbContext.Entry(product).State = EntityState.Modified;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAsyncProducts(IEnumerable<int> ids)
        {
            return await _dbContext.Products.Where(ci => ids.Contains(ci.Id)).ToListAsync();
        }

        public async Task<List<Product>> GetAsyncProductsItemsForPage(int pageSize, int pageIndex)
        {
            return await _dbContext.Products
                .OrderBy(c => c.Name)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
