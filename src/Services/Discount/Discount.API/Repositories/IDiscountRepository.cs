using System.Threading.Tasks;
using Discount.API.Entities;

namespace Discount.API.Repositories
{
	public interface IDiscountRepository
	{
		Task<Coupon> GetDiscount(string productName);
		Task<bool> CreateDiscount(Coupon coupon);
		Task<bool> UpdateDiscount(Coupon productName);
		Task<bool> DeleteDiscount(string productName);
	}
}