using Basket.API.Entities;
using Basket.API.Ropositories.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Ropositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _distributedCache;

        public Task DeleteBasketByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public Task<Cart?> GetBasketByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public Task<Cart> UpdateBasket(Cart cart, DistributedCacheEntryOptions options = null)
        {
            throw new NotImplementedException();
        }
    }
}
