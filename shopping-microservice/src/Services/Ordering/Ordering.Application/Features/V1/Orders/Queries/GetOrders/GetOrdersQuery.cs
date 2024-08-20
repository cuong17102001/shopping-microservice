using BuildingBlocks.Core.SeedWork;
using MediatR;
using Ordering.Application.Common.Models;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.V1.Orders.Queries.GetOrders;

public class GetOrdersQuery : IRequest<ApiResult<List<OrderDto>>>{
    public string UserName { get; set; }
    public GetOrdersQuery(string username){
        UserName = username ?? throw new ArgumentNullException(nameof(username));
    }
}