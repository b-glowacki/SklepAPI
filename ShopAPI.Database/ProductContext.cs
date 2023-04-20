using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopAPI.Database
{
	/// <summary>
	///		Class responsible for database.
	/// </summary>
	public class ProductContext : DbContext
	{
		public ProductContext(DbContextOptions<ProductContext> options)
			: base(options)
		{
		}
		
		public override int SaveChanges()
		{
			UpdatePropertiesOnChange();
			return base.SaveChanges();
		}

		public override int SaveChanges(bool acceptAllChangesOnSuccess)
		{
			UpdatePropertiesOnChange();
			return base.SaveChanges(acceptAllChangesOnSuccess);
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			UpdatePropertiesOnChange();
			return base.SaveChangesAsync(cancellationToken);
		}

		public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
		{
			UpdatePropertiesOnChange();
			return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		}

		/// <summary>
		///		Updates entity based on change state.
		/// </summary>
		private void UpdatePropertiesOnChange()
		{
			foreach(EntityEntry entry in ChangeTracker.Entries())
			{
				if (entry.Entity is not Product product)
				{
					continue;
				}

				switch (entry.State)
				{
					case EntityState.Added:
						product.EditionTime = DateTime.UtcNow;
						product.CreationTime = DateTime.UtcNow;
						break;

					case EntityState.Modified:
						product.EditionTime = DateTime.UtcNow;
						break;
				}
			}
		}

		public DbSet<Product> Products { get; set; }
	}
}
