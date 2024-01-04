using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProductService.Models;
using ProductService.Models.ViewModels;
using ProductService.Settings;

namespace ProductService.Services
{
    public class ProductServices
    {
        private readonly IMongoCollection<Product> _productCollection;

        public ProductServices(
            IOptions<MongoDbConfig> mongoDbConfig)
        {
            var mongoClient = new MongoClient(
                mongoDbConfig.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                mongoDbConfig.Value.DatabaseName);

            _productCollection = mongoDatabase.GetCollection<Product>(
                mongoDbConfig.Value.CollectionName);
        }

        public async Task<List<Product>> GetProductsAsync(string userId) =>
            await _productCollection.Find(p=>p.UserId==userId).ToListAsync();

        public async Task<Product?> GetProductByIdAsync(string id) =>
            await _productCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<Product?> CreateAsync(ProductVM newProduct)
        {
            var product = new Product
                      {
                UserId = newProduct.UserId,
                ProductName = newProduct.ProductName,
                Pcode = newProduct.Pcode,
                Category = newProduct.Category,
                Price = newProduct.Price,
                Quantity = newProduct.Quantity,
                Supplier = newProduct.Supplier,
                CreatedDate = DateTime.Today,
                ExpireDate = DateTime.Today,
            };
            await _productCollection.InsertOneAsync(product);

            return product;

        }

        public async Task UpdateAsync(string id, ProductVM updatedProduct)
        {
            var oldProduct= await _productCollection.Find(x=>x.Id==id).FirstOrDefaultAsync();
            var newProduct = new Product
            {
                Id = oldProduct.Id,
                UserId = oldProduct.UserId,
                ProductName = updatedProduct.ProductName,
                Pcode = updatedProduct.Pcode,
                Category = updatedProduct.Category,
                Price = updatedProduct.Price,
                Quantity = updatedProduct.Quantity,
                Supplier = updatedProduct.Supplier,
                CreatedDate = oldProduct.CreatedDate,
                ExpireDate = oldProduct.ExpireDate

            };

            await _productCollection.ReplaceOneAsync(x => x.Id == id,newProduct);

        }
            

        public async Task RemoveAsync(string id) =>
            await _productCollection.DeleteOneAsync(x => x.Id == id);
    }
}
