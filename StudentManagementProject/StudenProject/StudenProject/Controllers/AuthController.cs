using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StudenProject.Model;
using StudenProject.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudenProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {

        private readonly IStudentRepositories studentRepositories;
        private readonly IMapper mapper;
        private readonly JwtOption _options;
        public AuthController(IStudentRepositories studentRepositories, IMapper mapper,IOptions<JwtOption>options)
        {
            this.studentRepositories = studentRepositories;
            this.mapper = mapper;
            _options=options.Value;

        }
        [HttpPost("login")]
        public  async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var user= await studentRepositories.GetUserByUserName(model.UserName);
             if(user is null)
            {
                return BadRequest(new { error = "username bulunamadı" });
            }
             if(user.Password != model.Password)
            {
                return BadRequest(new { error = "şifra bulunamadı" });
            }
             var jwtKey=new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.Key));
            var credential=new SigningCredentials(jwtKey,SecurityAlgorithms.HmacSha256);
            List<Claim> claims = new List<Claim>()
            {
                new Claim("UserName",model.UserName),

            };
            var sToken=new JwtSecurityToken(_options.Key,_options.Issuer,claims,expires:DateTime.Now.AddHours(5),signingCredentials:credential);
            var token = new JwtSecurityTokenHandler().WriteToken(sToken);
            return Ok(new {token=token});

        }
    }
}
