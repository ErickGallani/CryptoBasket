namespace CryptoBasket.Api.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IdentityModel.Tokens.Jwt;
    using System.Text;

    /// <summary>
    /// IMPORTANT!!!! This controller is only to simplify the token generation for the sake of this application
    /// in a real word application the token would be probably handled by an Indentity Server or something similar
    /// </summary>
    [ApiController]
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/tokens")]
    [ExcludeFromCodeCoverage]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration config;

        public TokenController(IConfiguration config) =>
            this.config = config;

        [HttpGet]
        public string Get()
        {
            var issuer = this.config["Jwt:Issuer"];
            var key = this.config["Jwt:Key"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer,
              issuer,
              null,
              expires: DateTime.UtcNow.AddMinutes(240),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}