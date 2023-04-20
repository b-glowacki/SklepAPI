using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Database
{
	/// <summary>
	///		Interface responsible for connecting with products table.
	/// </summary>
	public interface IRepository
	{   
		/// <summary>
		///		Gets product by id.
		/// </summary>
		Task<Product> GetByIdAsync(int id);

		/// <summary>
		///		Gets all products.
		/// </summary>
		Task<IEnumerable<Product>> GetAllAsync();

		/// <summary>
		///		Creates a product.
		/// </summary>
		Task<Product> CreateAsync(Product product);

		/// <summary>
		///		Updates a product.
		/// </summary>
		Task UpdateAsync(Product product);

		/// <summary>
		///		Deletes a product by id.
		/// </summary>
		Task DeleteAsync(int id);
	}

	public class Repository : IRepository
	{
		private readonly ProductContext _productContext;

		public Repository(ProductContext productContext)
		{
			_productContext = productContext;
		}

		public async Task<Product> CreateAsync(Product product)
		{
			var createdProduct = await _productContext.Products.AddAsync(product);
			await _productContext.SaveChangesAsync();
			return createdProduct.Entity;
		}

		public async Task DeleteAsync(int id)
		{
			Product product = new Product() { Id = id };
			_productContext.Products.Attach(product);
			_productContext.Products.Remove(product);
			await _productContext.SaveChangesAsync();
		}

		public async Task<IEnumerable<Product>> GetAllAsync()
		{
			return await _productContext.Products.ToListAsync();
		}

		public async Task<Product> GetByIdAsync(int id)
		{
			return await _productContext.Products.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task UpdateAsync(Product product)
		{
			_productContext.Products.Update(product);
			await _productContext.SaveChangesAsync();
		}
	}
}
