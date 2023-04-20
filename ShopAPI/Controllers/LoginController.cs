using Microsoft.AspNetCore.Mvc;
using ShopAPI.Models;
using ShopAPI.Services;
using System.Security.Claims;

namespace ShopAPI.Controllers
{
    /// <summary>
    ///		Controller responsible for user login authorization.
    /// </summary>
    [ApiController]
	[Route("login")]
	public class LoginController : Controller
	{
		private readonly ITokenGenerator _tokenGenerator;

		public LoginController(ITokenGenerator tokenGenerator)
		{
			_tokenGenerator = tokenGenerator;
		}

        /// <summary>
        ///		Creates a JWT token based on e-mail and password provided by user.
        /// </summary>
        [HttpPost]
        public IActionResult Login([FromBody] LoginForm form)
		{
            if (string.IsNullOrWhiteSpace(form.Email))
            {
                return BadRequest("Provided e-mail is empty or containts white space");
            }

            if (string.IsNullOrWhiteSpace(form.Password))
			{
				return BadRequest("Provided password is empty or containts white space");
			}

			// Due to not having a table with user accounts we ignore validation of a user's existance in database.
			var emailClaim = new Claim("email", form.Email);
			string jwtToken = _tokenGenerator.CreateJwtToken(emailClaim);

			return Ok(jwtToken);
		}
	}
}
