using Lamar;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.API.Configuration
{
	public static class RedisConfiguration
	{
		public static void ConfigureRedis(this ServiceRegistry services, IConfiguration configuration)
		{
			services.AddStackExchangeRedisCache(option =>
			{
				option.Configuration = configuration["Redis:Connection"];
			});
		}
	}
}