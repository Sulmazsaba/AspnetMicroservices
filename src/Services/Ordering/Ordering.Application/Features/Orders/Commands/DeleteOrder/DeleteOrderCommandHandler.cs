using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {

        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CheckoutOrderCommandHandler> _logger;

        public DeleteOrderCommandHandler(ILogger<CheckoutOrderCommandHandler> logger, IMapper mapper, IOrderRepository orderRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var orderToDelete =await _orderRepository.GetByIdAsync(request.Id);

            if (orderToDelete == null)
            {
                _logger.LogError($"Order {request.Id} not exist in the database");
            }

            await _orderRepository.DeleteAsync(orderToDelete);

            _logger.LogInformation($"{orderToDelete.Id} deleted successfully.");

            return Unit.Value;
            
        }
    }
}
