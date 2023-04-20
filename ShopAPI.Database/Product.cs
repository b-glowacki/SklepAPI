using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Database
{  
	/// <summary>
	///		Class responsible for data related to products.
	/// </summary>
	public class Product
	{
		/// <summary>
		///		Product's id in database.
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		/// <summary>
		///		Product's name in database.
		/// </summary>
		public string ProductName { get; set; }

		/// <summary>
		///		Product's creation time.
		/// </summary>
		public DateTime CreationTime { get; set; }

		/// <summary>
		///		Product's property edition time.
		/// </summary>
		public DateTime EditionTime { get; set; }

		/// <summary>
		///		Product's description.
		/// </summary>
		public string ProductDescription { get; set; }

		/// <summary>
		///		Product's price.
		/// </summary>
		public float Price { get; set; }
	}
}
