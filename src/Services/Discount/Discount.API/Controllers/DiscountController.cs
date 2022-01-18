using System.Threading.Tasks;
using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public class DiscountController : Controller
	{
		private readonly IDiscountRepository _discountRepository;

		public DiscountController(IDiscountRepository discountRepository)
		{
			_discountRepository = discountRepository;
		}

		[HttpGet(Name = nameof(GetDiscount))]
		[ProducesResponseType(typeof(Coupon), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(Coupon), StatusCodes.Status404NotFound)]
		public async Task<ActionResult<Coupon>> GetDiscount([FromQuery] string productName)
		{
			var coupon = await _discountRepository.GetDiscount(productName);
			return Ok(coupon);
		}

		[HttpPost]
		[ProducesResponseType(typeof(Coupon), StatusCodes.Status201Created)]
		public async Task<ActionResult<Coupon>> CreateDiscount([FromBody] Coupon coupon)
		{
			var isCreated = await _discountRepository.CreateDiscount(coupon);
			return Ok(isCreated);
		}

		[HttpPut]
		[ProducesResponseType(typeof(Coupon), StatusCodes.Status201Created)]
		public async Task<ActionResult<Coupon>> UpdateDiscount([FromBody] Coupon coupon)
		{
			var isUpdated = await _discountRepository.UpdateDiscount(coupon);
			return Ok(isUpdated);
		}

		[HttpDelete]
		[ProducesResponseType(typeof(Coupon), StatusCodes.Status201Created)]
		public async Task<ActionResult<Coupon>> DeleteDiscount([FromQuery] string productName)
		{
			var isDeleted = await _discountRepository.DeleteDiscount(productName);
			return Ok(isDeleted);
		}
	}
}