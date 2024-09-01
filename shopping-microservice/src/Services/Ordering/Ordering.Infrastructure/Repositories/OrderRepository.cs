using Contracts.Common.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Common.Interfaces;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistence;

namespace Ordering.Infrastructure.Repositories;
public class OrderRepository : RepositoryBaseAsync<Order, long, OrderingDbContext>, IOrderRepository
{
    
    public OrderRepository(OrderingDbContext orderContext, IUnitOfWork<OrderingDbContext> unitOfWork)
        : base(orderContext, unitOfWork){

    }

    public async Task<long> AddOrderAsync(Order order)
    {
        var result = await CreateAsync(order);
        await SaveChangesAsync();
        return result;
    }

    public async Task<IEnumerable<Order>> GetOrderByUserName(string userName) => await FindByCondition(x => x.UserName == userName).ToListAsync();
    
}