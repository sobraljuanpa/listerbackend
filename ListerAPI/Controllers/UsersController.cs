using ListerAPI.Models;
using ListerAPI.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ListerAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController
    {
        private IUserService service;
        public UsersController(IUserService userService)
        {
            service = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public ActionResult Login([FromBody] User user)
        {
            User aux = service.GetUsers().Find(u => user.UserName.Equals(u.UserName));
            if (aux != null)
            {
                var key = Encoding.ASCII.GetBytes("Testing key tiene que ser mas larga");

                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, aux.UserName)
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(10),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return new AcceptedResult("",tokenHandler.WriteToken(token));
            }

            return new UnauthorizedResult();
        }

        [HttpGet]
        public ActionResult<List<User>> GetUsers()
        {
            return service.GetUsers();
        }
    }
}
