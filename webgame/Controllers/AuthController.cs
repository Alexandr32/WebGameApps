using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApplication.Model.Auth;
using WebApplicationGame.Core;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication.Controllers
{
    //https://www.youtube.com/watch?v=pq-zIs5CFQI&t=396s
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IOptions<AuthOption> _options;
        private List<Account> Account => new List<Account>()
        {
            new Account
            {
                Id = Guid.Parse("cd4fe988-b91c-4484-8b71-087d2b50ae82"),
                Mail = "user@mail.ru",
                Password = "user",
                Roles = new Role[] { Role.User }
            },
             new Account
            {
                Id = Guid.Parse("f7a4c2f4-38ea-45e7-b331-08dd202f3a82"),
                Mail = "user2@mail.ru",
                Password = "user2",
                Roles = new Role[] { Role.User }
            },
              new Account
            {
                Id = Guid.Parse("b9a1b2a7-ebae-44b3-9626-efb61f034878"),
                Mail = "admin@mail.ru",
                Password = "admin",
                Roles = new Role[] { Role.User }
            }
        };

        public AuthController(IOptions<AuthOption> options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        [Route("Login")]
        [HttpPost]
        public IActionResult Login([FromBody] Login login)
        {
            var user = AuthenticationUser(login);

            if (user != null)
            {
                var token = GetJwtToken(user);
                return Ok(new {
                    access_token = token
                });
            }

            return Unauthorized();
        }

        private Account AuthenticationUser(Login login)
        {
            return Account.SingleOrDefault(s => s.Mail == login.Mail && s.Password == login.Password);
        }

        // https://www.youtube.com/watch?v=pq-zIs5CFQI&t=396s
        // 16:23
        /// <summary>
        /// Получить токн который содержит в себе данные
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        private string GetJwtToken(Account account)
        {
            var authOprion = _options.Value;
            var securityKey = authOprion.GetSymmetricSecurityKey();

            var credenttials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Записываем поля
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, account.Mail),
                new Claim(JwtRegisteredClaimNames.Sub, account.Id.ToString())
            };

            foreach (var role in account.Roles) 
            {
                claims.Add(new Claim("role", role.ToString()));
            }

            var token = new JwtSecurityToken(authOprion.Issuer,
                authOprion.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authOprion.TokenLifeTime), 
                signingCredentials: credenttials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
           
        }

    }
}
