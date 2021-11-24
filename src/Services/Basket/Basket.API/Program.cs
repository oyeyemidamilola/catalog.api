using System;
using System.Reflection;
using Lamar.Microsoft.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Basket.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine(Assembly.GetEntryAssembly());
			CreateHostBuilder(args)
				.UseLamar()
				.Build()
				.Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
