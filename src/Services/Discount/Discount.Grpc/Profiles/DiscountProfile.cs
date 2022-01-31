using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;

namespace Discount.Grpc.Profiles
{
	public class DiscountProfile : Profile
	{
		public DiscountProfile()
		{
			CreateMap<Coupon, CouponModel>()
				.ForMember(cm => cm.Id, opt => opt.MapFrom(c => c.Id))
				.ForMember(cm => cm.ProductName, opt => opt.MapFrom(c => c.ProductName))
				.ForMember(cm => cm.Amount, opt => opt.MapFrom(c => c.Amount))
				.ForMember(cm => cm.Description, opt => opt.MapFrom(c => c.Description));

			CreateMap<CouponModel, Coupon>()
				.ForMember(c => c.Id, opt => opt.MapFrom(cm => cm.Id))
				.ForMember(c => c.ProductName, opt => opt.MapFrom(cm => cm.ProductName))
				.ForMember(c => c.Amount, opt => opt.MapFrom(cm => cm.Amount))
				.ForMember(c => c.Description, opt => opt.MapFrom(cm => cm.Description));

		}
	}
}