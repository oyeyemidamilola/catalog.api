using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
	public class BasketRepository : IBasketRepository
	{
		private readonly IDistributedCache _cache;

		public BasketRepository(IDistributedCache cache)
		{
			_cache = cache;
		}
		public async Task<Cart> GetBasket(string userName)
		{
			var basket = await _cache.GetStringAsync(userName);
			return !string.IsNullOrEmpty(basket) ? JsonConvert.DeserializeObject<Cart>(basket) : null; 
		}

		public async Task<Cart> UpdateBasket(Cart cart)
		{
			await _cache.SetStringAsync(cart.UserName, JsonConvert.SerializeObject(cart));
			return await GetBasket(cart.UserName);
		}

		public async Task DeleteBasket(string userName)
		{
			await _cache.RemoveAsync(userName);
		}
	}
}