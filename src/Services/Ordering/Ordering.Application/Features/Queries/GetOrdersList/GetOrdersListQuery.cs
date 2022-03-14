using System.Collections.Generic;
using MediatR;

namespace Ordering.Application.Features.Queries.GetOrdersList
{
	public class GetOrdersListQuery : IRequest<List<OrdersVm>>
	{
		public string UserName { get; set; }
	}
}