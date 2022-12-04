using MongoDB.Driver;
using Strategy.DesignPattern.WebApp.Entities;

namespace Strategy.DesignPattern.WebApp.Repositories
{
    public class ProductRepositoryFromMongoDb : IProductRepository
    {
        private readonly IMongoCollection<Product> _productCollection;

        public ProductRepositoryFromMongoDb(IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("MongoDb");

            MongoClient? client = new MongoClient(connectionString);

            IMongoDatabase? database = client.GetDatabase("ProductDb");

            _productCollection = database.GetCollection<Product>("Products");
        }

        public async Task<Product> Add(Product product)
        {
            await _productCollection.InsertOneAsync(product);
            return product;
        }

        public async Task Delete(Product product)
        {
            await _productCollection.DeleteOneAsync(x=>x.Id==product.Id);
        }

        public async Task<List<Product>> GetAllByUserId(string userId)
        {
            return await _productCollection.Find(x => x.UserId == userId).ToListAsync();
        }

        public async Task<Product> GetById(string id)
        {
            return await _productCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task Update(Product product)
        {
            await _productCollection.FindOneAndReplaceAsync(x => x.Id == product.Id, product);
        }
    }
}
