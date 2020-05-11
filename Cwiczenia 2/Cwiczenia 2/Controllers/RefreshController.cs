using Cwiczenia2.DTO;
using Cwiczenia2.Logic;
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
using System.Web.Providers.Entities;

namespace Cwiczenia2.Controllers
{
    [Route("api/refresh")]
    [ApiController]
    public class RefreshController : ControllerBase
{
        private readonly IConfiguration configuration;
        private readonly ISecurityDb securityDb;

        public RefreshController(IConfiguration configuration, ISecurityDb securityDb)
        {
            this.securityDb = securityDb;
            this.configuration = configuration;
        }

        [HttpPost]
        public IActionResult RefreshToken(RefreshReq refreshReq)
        {
            if (!securityDb.checkIfTokenExists(refreshReq.refreshToken))
            {
                return NotFound();
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


            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken = rToken
            });
        }

    }
}
