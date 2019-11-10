using System;
using System.Threading.Tasks;

namespace Product.Domain.Interfaces
{
    /// <summary>
    /// Interface IProductRepository
    /// </summary>
    public interface IProductRepository
    {
        Task<Models.Product> GetByProductId(Guid productId);

        Task Add(Models.Product product);

        Task Update(Models.Product product);
    }
}