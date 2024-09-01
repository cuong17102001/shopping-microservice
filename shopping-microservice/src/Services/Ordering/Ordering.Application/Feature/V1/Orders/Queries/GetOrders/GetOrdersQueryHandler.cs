using AutoMapper;
using BuildingBlocks.Core.SeedWork;
using MediatR;
using Ordering.Application.Common.Interfaces;
using Ordering.Application.Common.Models;

namespace Ordering.Application.Feature.V1.Orders.Queries.GetOrders
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, ApiResult<List<OrderDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        public GetOrdersQueryHandler(IMapper mapper, IOrderRepository orderRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }
        public async Task<ApiResult<List<OrderDto>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var orderEntities = await _orderRepository.GetOrderByUserName(request.UserName);
            var result = new List<OrderDto>();

            foreach(var orderEntity in orderEntities)
            {
                result.Add(new OrderDto
                {
                    Id = orderEntity.Id,
                    UserName = orderEntity.UserName,
                    FirstName = orderEntity.FirstName,
                    LastName = orderEntity.LastName,
                    TotalPrice = orderEntity.TotalPrice,
                    EmailAddress = orderEntity.EmailAddress,
                    InvoiceAddress = orderEntity.InvoiceAddress,
                    ShippingAddress = orderEntity.ShippingAddress,
                    Stutus = orderEntity.Status.ToString()
                });
            }

            return new ApiSuccessResult<List<OrderDto>>(result);
        }
    }
}
