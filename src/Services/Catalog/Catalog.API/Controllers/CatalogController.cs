using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Catalog.API.Controllers
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public class CatalogController : ControllerBase
	{
		private readonly IProductRepository _productRepository;

		public CatalogController(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
		{
			return  Ok(await _productRepository.GetProducts());
		}

		[HttpGet("{id:length(24)}", Name = "GetProduct")]
		[ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(Product), StatusCodes.Status404NotFound)]
		public async Task<ActionResult<Product>> GetProduct(string id)
		{
			var result = await _productRepository.GetProduct(id);
			return result == null ? (ActionResult<Product>) NotFound() : Ok(result);
		}

		[HttpGet("[action]/{category}")]
		[ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category)
		{
			var result = await _productRepository.GetProductByCategory(category);
			return Ok(result);
		}

		[HttpPost]
		[ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
		public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
		{
			await _productRepository.CreateProduct(product);
			return CreatedAtRoute("GetProduct", new { id = product.Id}, product);
		}

		[HttpPut]
		[ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
		public async Task<IActionResult> UpdateProduct([FromBody] Product product)
		{
			return Ok(await _productRepository.UpdateProduct(product));
		}

		[HttpDelete("{id:length(24)}")]
		[ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
		public async Task<IActionResult> DeleteProduct(string id)
		{
			return Ok(await _productRepository.DeleteProduct(id));
		}




	}
}