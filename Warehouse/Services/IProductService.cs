using Warehouse.Models;

namespace Warehouse.Services
{
    public interface IProductService
    {
        public Task<int> Create(Product product);
        public Task<Product> Read(int id);
        public Task<bool> Update(Product product);
        public Task<bool> Delete(int id);
    }
}
