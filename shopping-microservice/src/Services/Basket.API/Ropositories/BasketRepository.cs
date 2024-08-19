using Basket.API.Entities;
using Basket.API.Ropositories.Interfaces;
using Contracts.Common.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using ILogger = Serilog.ILogger;

namespace Basket.API.Ropositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCacheService;
        private readonly ISerializeService _serializerService;
        private readonly ILogger _logger;

        public BasketRepository(IDistributedCache distributedCache, ISerializeService serializeService, ILogger logger)
        {
            _redisCacheService = distributedCache;
            _serializerService = serializeService;
            _logger = logger;
        }

        public async Task DeleteBasketByUsername(string username)
        {
            try
            {
                await _redisCacheService.RemoveAsync(username);
            }
            catch (System.Exception e)
            {
                _logger.Error("DeleteBasketByUsername", e);
                throw;
            }
        }

        public async Task<Cart?> GetBasketByUsername(string username)
        {
            var basket = await _redisCacheService.GetStringAsync(username);
            return string.IsNullOrEmpty(basket) ? null : _serializerService.Deserialize<Cart>(basket);
        }

        public async Task<Cart> UpdateBasket(Cart cart, DistributedCacheEntryOptions options = null)
        {
            if (options != null)
            {
                await _redisCacheService.SetStringAsync(cart.Username, _serializerService.Serialize(cart), options);
            }else{
                await _redisCacheService.SetStringAsync(cart.Username, _serializerService.Serialize(cart));
            }

            return await GetBasketByUsername(cart.Username);
        }
    }
}
