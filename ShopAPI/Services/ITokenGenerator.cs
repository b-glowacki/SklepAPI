using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShopAPI.Services
{
    /// <summary>
    ///		Interfrace responsible for JWT token creation.
    /// </summary>
    public interface ITokenGenerator
	{
        /// <summary>
        ///		Creates JWT token based on provided claims.
        /// </summary>
        string CreateJwtToken(params Claim[] claims);
	}

	public class TokenGenerator : ITokenGenerator
	{
		private const string SigningKeyAlgorithm = SecurityAlgorithms.HmacSha256Signature;
		private const string SigningKeyAlgorithmShortName = "HS256";
		private readonly string _audience;
		private readonly string _issuer;
		private readonly SecurityKey _securityKey;
        private readonly TimeSpan _tokenLifeTime;

		public TokenGenerator(string jwtPrivateKey, string issuer = null, string audience = null, TimeSpan? tokenLifeTime = null)
		{
			_securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtPrivateKey.ToSha512()));
            _issuer = issuer ?? "https://www.lot.com";
			_audience = audience ?? "https://www.lot.com";
			_tokenLifeTime = tokenLifeTime ?? TimeSpan.FromHours(1);
		}

		public string CreateJwtToken(params Claim[] claims)
		{
			var handler = new JwtSecurityTokenHandler();

			var claimsIdentity = new ClaimsIdentity(claims);
			SecurityToken securityToken = handler.CreateToken(new SecurityTokenDescriptor
			{
				Subject = claimsIdentity,
				Issuer = _issuer,
				Audience = _audience,
				Expires = DateTime.UtcNow + _tokenLifeTime,
				SigningCredentials = new SigningCredentials(_securityKey, SigningKeyAlgorithm)
			});

			// Return generated JWT token.
			return handler.WriteToken(securityToken);
		}
	}
}
