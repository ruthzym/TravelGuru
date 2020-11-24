using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Saitynai.Models;
using Saitynai.Options;
using Saitynai.Repository;

namespace Saitynai.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IOptions<AuthOptions> options;

        internal MongoDBContext db = new MongoDBContext();

        public AuthController(IOptions<AuthOptions> auth)
        {
            this.options = auth;
        }

        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] UserInfo request)
        {
            var user = AuthenticateUser(request.Username, request.Password);

            if (user != null)
            {

                //Generate JWT
                var token = GenerateJWT(user);

                return Ok(new
                {
                    access_token = token
                });
            }

            return Unauthorized();
        }

        private User AuthenticateUser(string username, string pass)
        {
            var fil = Builders<User>.Filter.Eq(x=> x.UserName, username);
            var fi = Builders<User>.Filter.Eq(x => x.Password, pass);
            return db.User.Find(fil & fi).FirstOrDefault();
        }

        private string GenerateJWT(User user)
        {
            var authParams = options.Value;

            var securitKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securitKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, user.UserName),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id)
            };

            foreach (var role in user.Roles)
            {
                claims.Add(new Claim("role", role.ToString()));
            }

            var token = new JwtSecurityToken(authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifeTime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
