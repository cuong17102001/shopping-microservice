using AutoMapper;
using Basket.API.Entities;
using Basket.API.Ropositories.Interfaces;
using EventBus.Messages.IntergrationEvents.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Net;

namespace Basket.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BasketsController : ControllerBase
{
    private readonly IBasketRepository _basketRepository;
    private readonly IPublishEndpoint _ublishEndpoint;
    private readonly IMapper _mapper;

    public BasketsController(IBasketRepository basketRepository, IPublishEndpoint publishEndpoint, IMapper mapper)
    {
        _basketRepository = basketRepository;
        _ublishEndpoint = publishEndpoint;
        _mapper = mapper;
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

    [Route("checkout")]
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Checkout(BasketCheckout basketCheckout)
    {
        var basket = await _basketRepository.GetBasketByUsername(basketCheckout.UserName);

        if (basket == null) return NotFound();

        var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);

        eventMessage.TotalPrice = basket.TotalPrice;
        await _ublishEndpoint.Publish(eventMessage);

        await _basketRepository.DeleteBasketByUsername(basketCheckout.UserName);

        return Accepted();
    }
}