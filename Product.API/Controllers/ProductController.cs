using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Model;
using ProductsAPI.Repository;
using Microsoft.EntityFrameworkCore;
using ProductAPI.ViewModel;

namespace ProductsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        // GET: api/Product
        [HttpGet]
        public IActionResult Get()
        {
            var products = _productRepository.GetProducts();
            return new OkObjectResult(products);
        }

        // GET: api/Product/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var product = _productRepository.GetProductByID(id);
            return new OkObjectResult(product);
        }

        // POST: api/Product
        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            using (var scope = new TransactionScope())
            {
                _productRepository.InsertProduct(product);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
            }
        }

        // PUT: api/Product/5
        [HttpPut]
        public IActionResult Put([FromBody] Product product)
        {
            if (product != null)
            {
                using (var scope = new TransactionScope())
                {
                    _productRepository.UpdateProduct(product);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _productRepository.DeleteProduct(id);
            return new OkResult();
        }

        [HttpGet]
        [Route("items/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> ItemByIdAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var item = await _productRepository.GetAsyncProductByID(id);

            //var baseUri = _settings.PicBaseUrl;
            //var azureStorageEnabled = _settings.AzureStorageEnabled;

            //item.FillProductUrl(baseUri, azureStorageEnabled: azureStorageEnabled);

            if (item != null)
            {
                return item;
            }

            return NotFound();
        }

        // GET api/v1/[controller]/items[?pageSize=3&pageIndex=10]
        [HttpGet]
        [Route("items")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<Product>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ItemsAsync([FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0, string ids = null)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var items = await GetItemsByIdsAsync(ids);

                if (!items.Any())
                {
                    return BadRequest("ids value invalid. Must be comma-separated list of numbers");
                }

                return Ok(items);
            }

            var totalItems = await _productRepository.GetAsyncProductCount();

            var itemsOnPage = await _productRepository.GetAsyncProductsItemsForPage(pageSize, pageIndex);

            /* The "awesome" fix for testing Devspaces */

            /*
            foreach (var pr in itemsOnPage) {
                pr.Name = "Awesome " + pr.Name;
            }

            */

            var model = new PaginatedItemsViewModel<Product>(pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);
        }

        //PUT api/v1/[controller]/items
        [Route("items")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> UpdateProductAsync([FromBody]Product productToUpdate)
        {
            var productItem = await _productRepository.GetAsyncProductByID(productToUpdate.Id);

            if (productItem == null)
            {
                return NotFound(new { Message = $"Item with id {productToUpdate.Id} not found." });
            }

            var oldPrice = productItem.Price;
            var raiseProductPriceChangedEvent = oldPrice != productToUpdate.Price;

            // Update current product
            productItem = productToUpdate;
            _productRepository.UpdateProduct(productItem);

            if (raiseProductPriceChangedEvent) // Save product's data and publish integration event through the Event Bus if price has changed
            {
                //Create Integration Event to be published through the Event Bus
               
            }
            else // Just save the updated product because the Product's Price hasn't changed.
            {
                await _productRepository.SaveChangesAsync();
            }

            return CreatedAtAction(nameof(ItemByIdAsync), new { id = productToUpdate.Id }, null);
        }

        private async Task<List<Product>> GetItemsByIdsAsync(string ids)
        {
            var numIds = ids.Split(',').Select(id => (Ok: int.TryParse(id, out int x), Value: x));

            if (!numIds.All(nid => nid.Ok))
            {
                return new List<Product>();
            }

            var idsToSelect = numIds
                .Select(id => id.Value);

            var items = await _productRepository.GetAsyncProducts(idsToSelect);

            return items;
        }
    }
}
