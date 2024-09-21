using AutoMapper;
using BuildingBlocks.Core.SeedWork;
using MediatR;
using Ordering.Application.Common.Interfaces;
using Ordering.Domain.Entities;
using ILogger = Serilog.ILogger;

namespace Ordering.Application.Feature.V1.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ApiResult<long>>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger _logger;

        public CreateOrderCommandHandler(IMapper mapper, IOrderRepository orderRepository, ILogger logger)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _logger = logger;
        }

        async Task<ApiResult<long>> IRequestHandler<CreateOrderCommand, ApiResult<long>>.Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.Information("Start add order");
            var order = new Order
            {
                UserName = request.UserName,
                TotalPrice = request.TotalPrice,
                FirstName = request.FirstName,
                LastName = request.LastName,
                ShippingAddress = request.ShippingAddress,
                EmailAddress = request.EmailAddress,
                InvoiceAddress = request.InvoiceAddress,
            };
            var result = _orderRepository.Create(order);
            order.AddedOrder();
            await _orderRepository.SaveChangesAsync();
            _logger.Information("End add order");
            return new ApiSuccessResult<long>(result);
        }
    }
}
