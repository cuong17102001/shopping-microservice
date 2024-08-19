using Basket.API.Entities;
using Basket.API.Ropositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BasketsController : ControllerBase
{
    private readonly IBasketRepository _basketRepository;

    public BasketsController(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }

    [HttpGet("{username}", Name = "GetBasket")]
    public async Task<IActionResult> GetBasketByUsername(string username)
    {
        var basket = await _basketRepository.GetBasketByUsername(username);
        if (basket == null)
        {
            return Ok(new Cart());
        }
        return Ok(basket);
    }

    [HttpPost(Name = "UpdateBasket")]
    public async Task<IActionResult> UpdateBasket([FromBody] Cart cart)
    {
        var options = new DistributedCacheEntryOptions()
        .SetAbsoluteExpiration(DateTime.UtcNow.AddHours(1))
        .SetSlidingExpiration(TimeSpan.FromMinutes(5));
        var basket = await _basketRepository.UpdateBasket(cart, options);
        return Ok(basket);
    }

    [HttpDelete("{username}", Name = "DeleteBasket")]
    public async Task<IActionResult> DeleteBasket(string username)
    {
        await _basketRepository.DeleteBasketByUsername(username);
        return Ok();
    }
}