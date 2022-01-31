using System.Threading.Tasks;
using Discount.Grpc.Protos;

namespace Basket.API.GrpcClients
{
	public class DiscountGrpcClient
	{
		private readonly DiscountService.DiscountServiceClient _discountServiceClient;
		public DiscountGrpcClient(DiscountService.DiscountServiceClient discountServiceClient)
		{
			_discountServiceClient = discountServiceClient;
		}

		public async Task<CouponModel> GetDiscount(string productName)
		{
			var discountRequest = new GetDiscountRequest{ ProductName =  productName };
			return _discountServiceClient.GetDiscount(discountRequest);
		}
	}
}