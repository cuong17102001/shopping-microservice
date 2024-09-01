using AutoMapper;
using EventBus.Messages.IntergrationEvents.Events;
using MassTransit;
using MediatR;
using Ordering.Application.Feature.V1.Orders.Commands.CreateOrder;
using ILogger = Serilog.ILogger;

namespace Ordering.API.Application.IntergrationEvents.EventHandlers
{
    public class BasketCheckoutEventHandler : IConsumer<BasketCheckoutEvent>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public BasketCheckoutEventHandler(IMediator mediator, IMapper mapper, ILogger logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            var commad = _mapper.Map<CreateOrderCommand>(context.Message);

            var result = await _mediator.Send(commad);
            _logger.Information("Basket checkout consume successfully!");
        }
    }
}
