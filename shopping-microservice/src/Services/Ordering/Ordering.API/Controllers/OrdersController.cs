using BuildingBlocks.Contracts.Services;
using BuildingBlocks.Shared.Services.Email;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Common.Models;
using Ordering.Application.Features.V1.Orders.Queries.GetOrders;

namespace Ordering.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IEmailService<MailRequest> _emailService;

        public OrdersController(IMediator mediator, IEmailService<MailRequest> emailService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _emailService = emailService ?? throw new ArgumentException(nameof(emailService));
        }

        private static class RouteNames
        {
            public const string GetOrders = nameof(GetOrders);
        }

        [HttpGet("{username}", Name = RouteNames.GetOrders)]
        [ProducesResponseType(typeof(IEnumerable<OrderDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrders([FromRoute] string username)
        {
            var query = new GetOrdersQuery(username);
            return Ok(await _mediator.Send(query));
        }


        [HttpGet("test")]
        [ProducesResponseType(typeof(IEnumerable<OrderDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Test()
        {
            var data = new MailRequest
            {
                ToAddress = "nqcuong.1710@gmail.com",
                Subject = "Test hihi",
                Body = "<h1>Test</h1>"
            };

            await _emailService.SendEmailAsync(data);

            return Ok(data);
        }
    }
}
