using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopAPI.Database;

namespace ShopAPI.Controllers
{
    /// <summary>
    ///		Controller responsible for product CRUD.
    /// </summary>
    [ApiController]
	[Route("product")]
	[Authorize]
	public class ProductController : Controller
	{
		private readonly IRepository _repository;

		public ProductController(IRepository repository)
		{
			_repository = repository;
		}

        /// <summary>
        ///		Gets a list of all products from the database.
        /// </summary>
        [HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
		{
			var products = await _repository.GetAllAsync();
			return Ok(products);
		}

		/// <summary>
		///		Gets a product based on id from the database.
		/// </summary>
		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProduct(int id)
		{
			var product = await _repository.GetByIdAsync(id);

			if (product == null)
			{
				return NotFound();
			}

			return Ok(product);
		}

        /// <summary>
        ///		Creates a new product in the database.
        /// </summary>
        [HttpPost]
		public async Task<ActionResult<Product>> CreateProduct([FromBody]Product product)
		{
			var createdProduct = await _repository.CreateAsync(product);

			return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, createdProduct);
		}

		/// <summary>
		///		Updates an existing product in the database.
		/// </summary>
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateProduct(int id, [FromBody]Product product)
		{
			var existingProduct = await _repository.GetByIdAsync(id);

			if (existingProduct == null)
			{
				return NotFound();
			}

			existingProduct.ProductName = product.ProductName;
			existingProduct.ProductDescription = product.ProductDescription;
			existingProduct.Price = product.Price;

			await _repository.UpdateAsync(existingProduct);

			return NoContent();
		}

        /// <summary>
        ///		Deletes a product from database.
        /// </summary>
        [HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			var existingProduct = await _repository.GetByIdAsync(id);

			if (existingProduct == null)
			{
				return NotFound();
			}

			await _repository.DeleteAsync(id);

			return NoContent();
		}
	}
}
