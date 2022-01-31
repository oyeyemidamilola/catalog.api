using System.Threading.Tasks;
using Basket.API.Entities;
using Basket.API.GrpcClients;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public class BasketController : ControllerBase
	{
		private readonly IBasketRepository _basketRepository;
		private readonly DiscountGrpcClient _discountGrpcClient;

		public BasketController(IBasketRepository basketRepository, DiscountGrpcClient discountGrpcClient)
		{
			_basketRepository = basketRepository;
			_discountGrpcClient = discountGrpcClient;
		}

		[HttpGet("{userName}", Name = nameof(GetBasket))]
		[ProducesResponseType(typeof(Cart), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(Cart), StatusCodes.Status200OK)]
		public async Task<ActionResult<Cart>> GetBasket(string userName)
		{
			if (string.IsNullOrEmpty(userName))
			{
				return BadRequest("Username can not be empty");
			}
			return Ok(await _basketRepository.GetBasket(userName));
		}

		[HttpPost]
		[ProducesResponseType(typeof(Cart), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(Cart), StatusCodes.Status201Created)]
		public async Task<ActionResult<Cart>> UpdateBasket([FromBody] Cart cart)
		{
			if (string.IsNullOrEmpty(cart.UserName))
			{
				return BadRequest("Username can not be empty");
			}

			foreach (var cartItem in cart.Items)
			{
				var coupon = await _discountGrpcClient.GetDiscount(cartItem.ProductName);
				cartItem.Price -= coupon.Amount;
			}
			var result = await _basketRepository.UpdateBasket(cart);
			return CreatedAtAction(nameof(GetBasket), new {userName = result.UserName}, result);
		}

		[HttpDelete]
		[ProducesResponseType(typeof(Cart), StatusCodes.Status200OK)]
		public async Task<IActionResult> DeleteBasket(string userName)
		{
			await _basketRepository.DeleteBasket(userName);
			return Ok();
		}

	}
}