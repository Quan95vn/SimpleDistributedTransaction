using System.Threading.Tasks;

namespace Product.Domain.Interfaces
{
    /// <summary>
    /// Interface IProductRepository
    /// </summary>
    public interface IProductRepository
    {
        Task Add(Models.Product product);
    }
}