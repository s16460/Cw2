using Cwiczenia2.DTO;
using Cwiczenia2.Logic;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Cwiczenia2.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
{
        private readonly IConfiguration configuration;
        private readonly ISecurityDb securityDb;

        public LoginController(IConfiguration configuration, ISecurityDb securityDb)
        {
            this.configuration = configuration;
            this.securityDb = securityDb;
        }

        [HttpPost]
        public IActionResult login(LoginReq loginReq)
        {

            string salt = securityDb.getSalt(loginReq.login);
            
            if(salt == null)
            {
                return BadRequest("salt doesnt exist for this user, u cant login");
            }

            var valueBytes = KeyDerivation.Pbkdf2(
                password: loginReq.password,
                salt: Encoding.UTF8.GetBytes(salt),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256);


            loginReq.password = Convert.ToBase64String(valueBytes);
          

            if (!securityDb.loginUser(loginReq))
            {
                return Unauthorized();
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Role, "Employee"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecretKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken
                (
                    issuer: "Program",
                    audience: "Ktokolwiek",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: credentials
                );

            var rToken = Guid.NewGuid();
            securityDb.createRefreshToken(rToken);
            

            return Ok(new {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken = rToken
            });

        }
    }
}
