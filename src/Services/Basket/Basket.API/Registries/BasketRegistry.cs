using Basket.API.Repositories;
using Lamar;

namespace Basket.API.Registries
{
	public class BasketRegistry : ServiceRegistry
	{
		public BasketRegistry()
		{
			For<IBasketRepository>().Use<BasketRepository>().Scoped();
		}
	}
}