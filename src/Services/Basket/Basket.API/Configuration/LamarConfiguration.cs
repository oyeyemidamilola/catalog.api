using Basket.API.Registries;
using Lamar;

namespace Basket.API.Configuration
{
	public static class LamarConfiguration
	{
		public static void ConfigureLamar(this ServiceRegistry services)
		{
			services.Scan(scan =>
			{
				scan.AssembliesAndExecutablesFromApplicationBaseDirectory(assembly => assembly.FullName != null && assembly.FullName.StartsWith("Basket"));
				scan.WithDefaultConventions();
			});

			services.IncludeRegistry<BasketRegistry>();
		}
	}
}