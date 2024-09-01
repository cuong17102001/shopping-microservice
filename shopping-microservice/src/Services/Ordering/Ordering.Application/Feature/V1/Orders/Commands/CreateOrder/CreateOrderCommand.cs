using AutoMapper;
using BuildingBlocks.Core.SeedWork;
using MediatR;
using Ordering.Application.Common.Mappings;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBus.Messages.IntergrationEvents.Events;

namespace Ordering.Application.Feature.V1.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<ApiResult<long>>, IMapFrom<Order>
    {
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string ShippingAddress { get; set; }
        public string InvoiceAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateOrderCommand, Order>()
                .ForMember(dest => dest.Status, opt => opt.Ignore());
            profile.CreateMap<BasketCheckoutEvent, CreateOrderCommand>();
        }
    }
}
