using Azure;
using CollegeApp.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public ActionResult Login(LoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please provide the UserName and Password");

            }

            LoginResponseDto response = new() { UserName = model.UserName };


            if (model.UserName == "madhu" && model.Password == "madhu")
            {
                var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWTSecret"));

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor()
                {

                    Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, model.UserName),
                         new Claim(ClaimTypes.Role, "Admin")

                    }), 
                    Expires = DateTime.Now.AddHours(4),
                    SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)


                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                response.Token = tokenHandler.WriteToken(token);
            }
            else
            {

                return Ok("Invalid UserName and Password");

            }


            return Ok(Response);
        }
    }
}
