using System.ComponentModel.DataAnnotations;

namespace ShopAPI.Models
{
	/// <summary>
	///		Class responsible for login form.
	/// </summary>
	public class LoginForm
	{
        /// <summary>
        ///		User's e-mail.
        /// </summary>
        [Required]
		public string Email { get; set; }

        /// <summary>
        ///		User's password.
        /// </summary>
        [Required]
		public string Password { get; set; }
	}
}
