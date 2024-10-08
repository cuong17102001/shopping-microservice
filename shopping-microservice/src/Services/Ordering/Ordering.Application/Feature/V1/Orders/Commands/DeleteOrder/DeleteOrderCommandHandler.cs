﻿using AutoMapper;
using BuildingBlocks.Core.SeedWork;
using MediatR;
using Ordering.Application.Common.Exceptions;
using Ordering.Application.Common.Interfaces;
using Ordering.Domain.Entities;
using Serilog;

namespace Ordering.Application.Feature.V1.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger _logger;


        public DeleteOrderCommandHandler(IMapper mapper, IOrderRepository orderRepository, ILogger logger)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = await _orderRepository.GetByIdAsync(request.Id);
            if (orderEntity == null) throw new NotFoundException();

            _orderRepository.Delete(orderEntity);
            orderEntity.DeletedOrder();
            await _orderRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
