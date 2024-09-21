using MediatR;
using Ordering.Domain.OrderAggregate.Events;
using Serilog;

namespace Ordering.Application.Feature.V1.Orders;

public class OrdersDomainHandler : INotificationHandler<OrderCreatedEvent>, INotificationHandler<OrderDeletedEvent>
{
    private readonly ILogger _logger;

    public OrdersDomainHandler(ILogger logger)
    {
        _logger = logger;
    }

    public Task Handle(OrderDeletedEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
