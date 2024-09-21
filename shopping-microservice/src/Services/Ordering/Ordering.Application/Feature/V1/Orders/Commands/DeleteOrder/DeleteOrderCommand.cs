using BuildingBlocks.Core.SeedWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Feature.V1.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommand : IRequest<Unit>
    {
        public long Id { get; set; }
    }
}
