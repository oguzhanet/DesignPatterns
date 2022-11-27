using Strategy.DesignPattern.WebApp.Entities;

namespace Strategy.DesignPattern.WebApp.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetById(string id);
        Task<List<Product>> GetAllByUserId(string userId);
        Task<Product> Add(Product product);
        Task Update(Product product);
        Task Delete(Product product);
    }
}
