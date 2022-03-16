using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Commands.UpdateOrder
{
	public class UpdateOrderCommanHandler : IRequestHandler<UpdateOrderCommand, Unit>
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IMapper _mapper;
		private readonly ILogger<UpdateOrderCommanHandler> _logger;

		public UpdateOrderCommanHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<UpdateOrderCommanHandler> logger)
		{
			_orderRepository = orderRepository;
			_mapper = mapper;
			_logger = logger;
		}
		public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
		{
			var orderToUpdate = await _orderRepository.GetByIdAsync(request.Id);
			if (orderToUpdate == null)
			{
				_logger.LogError("Order not exist on database.");
			}

			_mapper.Map(request, orderToUpdate, typeof(UpdateOrderCommand), typeof(Order));
			await _orderRepository.UpdateAsync(orderToUpdate);
			_logger.LogInformation($"Order {orderToUpdate.Id} is successfully updated");
			return Unit.Value;
		}
	}
}