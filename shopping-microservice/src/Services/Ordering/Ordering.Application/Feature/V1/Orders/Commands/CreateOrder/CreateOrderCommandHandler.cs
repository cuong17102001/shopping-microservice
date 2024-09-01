using AutoMapper;
using BuildingBlocks.Core.SeedWork;
using MediatR;
using Ordering.Application.Common.Interfaces;
using Ordering.Domain.Entities;

namespace Ordering.Application.Feature.V1.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ApiResult<long>>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;

        public CreateOrderCommandHandler(IMapper mapper, IOrderRepository orderRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        async Task<ApiResult<long>> IRequestHandler<CreateOrderCommand, ApiResult<long>>.Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
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
            var result = await _orderRepository.AddOrderAsync(order);
            return new ApiSuccessResult<long>(result);
        }
    }
}
