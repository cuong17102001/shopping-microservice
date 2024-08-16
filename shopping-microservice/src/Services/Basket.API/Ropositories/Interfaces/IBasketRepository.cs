using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Ropositories.Interfaces
{
    public interface IBasketRepository
    {
        Task<Cart?> GetBasketByUsername(string username);
        Task<Cart> UpdateBasket(Cart cart, DistributedCacheEntryOptions options = default);
        Task DeleteBasketByUsername(string username);
    }
}
