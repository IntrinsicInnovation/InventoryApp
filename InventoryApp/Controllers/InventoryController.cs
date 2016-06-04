using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using InventoryApp.Models;
using System.Collections.Concurrent;
using Microsoft.AspNet.SignalR;

namespace InventoryApp.Controllers
{
    public class InventoryController : ApiWithHubController<NotificationHub> //ApiController
    {
        List<Product> products = new List<Product>();

        //static ConcurrentDictionary<string, Product> _products = new ConcurrentDictionary<string, Product>();
        //Could make it more efficient with a Dictionary implementation instead of a list.


        public InventoryController() {
            //Need data for testing with UI.
            var testProducts = new List<Product>();
            testProducts.Add(new Product { Id = 1, Name = "Demo1", Price = 1, Expired = false });
            testProducts.Add(new Product { Id = 2, Name = "Demo2", Price = 3.75M, Expired = true });
            testProducts.Add(new Product { Id = 3, Name = "Demo3", Price = 16.99M, Expired = false });
            testProducts.Add(new Product { Id = 4, Name = "Demo4", Price = 11.00M, Expired = true });
            this.products = testProducts;

        }

        public InventoryController(List<Product> products)
        {
            this.products = products;
        }

        
        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await Task.FromResult(GetAllProducts());
        }

        public IHttpActionResult GetProduct(int id)
        {
            var product = products.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        public async Task<IHttpActionResult> GetProductAsync(int id)
        {
            return await Task.FromResult(GetProduct(id));
        }


        // POST: api/Products
        public IHttpActionResult PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            this.products.Add(product);
         
            return CreatedAtRoute("DefaultApi", new { id = product.Id }, product);
        }


        // DELETE: api/Products/5
        public IHttpActionResult DeleteProduct(int id)
        {
            Product product = products.Find(p => p.Id == id); 
            if (product == null)
            {
                return NotFound();
            }

            this.products.Remove(product);
            var subscribed = Hub.Clients.Group(product.Id.ToString());
            subscribed.deleteItem(product);
            Hub.Clients.All.update("Item " + product.Name + " was deleted");

            return Ok(product);
        }


        public IEnumerable<Product> GetExpiredProducts()
        {
            var expiredNames = from p in products.Where(p => p.Expired)
                               select p; 

            return expiredNames;
        }

    }
}
