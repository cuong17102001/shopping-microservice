using System.Reflection;
using Contracts.Common.Events;
using Contracts.Common.Interfaces;
using Contracts.Domains.Interfaces;
using Infrastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistence;
public class OrderingDbContext : DbContext
{
    private readonly IMediator _mediator;
    public OrderingDbContext(DbContextOptions<OrderingDbContext> options, IMediator mediator) : base(options)
    {
        _mediator = mediator;
    }

    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    private List<BaseEvent> GetBaseEvents()
    {
        var domainEntities = ChangeTracker.Entries<IEventEntity>()
                .Select(e => e.Entity)
                .Where(e => e.GetDomainEvents().Any()).ToList();

        var domainEvents = domainEntities
            .SelectMany(e => e.GetDomainEvents()).ToList();

        domainEntities.ForEach(e => e.ClearDomainEvent());

        return domainEvents;
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var domainEvents = GetBaseEvents();
        var modified = ChangeTracker.Entries()
            .Where(e => e.State is EntityState.Modified or EntityState.Added or EntityState.Deleted);

        foreach (var item in modified)
        {
            switch (item.State)
            {
                case EntityState.Added:
                    if (item.Entity is IDateTracking addedEntity)
                    {
                        addedEntity.CreatedDate = DateTime.UtcNow;
                        item.State = EntityState.Added;
                    }
                    break;
                case EntityState.Modified:
                    Entry(item.Entity).Property("Id").IsModified = false;
                    if (item.Entity is IDateTracking modifiedEntity)
                    {
                        modifiedEntity.LastModifiedDate = DateTime.UtcNow;
                        item.State = EntityState.Modified;
                    }
                    break;

            }
        }

        var result = base.SaveChangesAsync(cancellationToken);

        _mediator.DispatchDomainEventAsync(domainEvents);

        return result;
    }
}