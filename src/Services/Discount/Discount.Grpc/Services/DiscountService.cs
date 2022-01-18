using System.Threading.Tasks;
using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Discount.Grpc.Services
{
	public class DiscountService : Protos.DiscountService.DiscountServiceBase
	{
		private readonly IDiscountRepository _discountRepository;
		private readonly IMapper _mapper;
		private readonly ILogger<DiscountService> _logger;

		public DiscountService(IDiscountRepository discountRepository, ILogger<DiscountService> logger)
		{
			_discountRepository = discountRepository;
			_logger = logger;
		}

		public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
		{
			var coupon = await _discountRepository.GetDiscount(request.ProductName);
			if (coupon == null)
			{
				throw new RpcException(new Status(StatusCode.NotFound, $"Discount with productName = {request.ProductName} not found"));
			}
			_logger.LogInformation($"Discount retrieved for ProductName: {coupon.ProductName}, Amount: {coupon.Amount}");
			return _mapper.Map<CouponModel>(coupon);
		}

		public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
		{
			var coupon = _mapper.Map<Coupon>(request.Coupon);
			await _discountRepository.CreateDiscount(coupon);
			_logger.LogInformation($"Discount successfully created. ProductName: {coupon.ProductName}");
			return _mapper.Map<CouponModel>(coupon);
		}

		public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
		{
			var coupon = _mapper.Map<Coupon>(request.Coupon);
			await _discountRepository.UpdateDiscount(coupon);
			_logger.LogInformation($"Discount successfully updated. ProductName: {coupon.ProductName}");
			return _mapper.Map<CouponModel>(coupon);
		}

		public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
		{
			var deleted = await _discountRepository.DeleteDiscount(request.ProductName);
			return new DeleteDiscountResponse
			{
				Success = deleted
			};
		}
	}
}