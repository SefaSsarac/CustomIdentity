using CustomIdentity.Dtos;
using CustomIdentity.Entities;
using CustomIdentity.Jwt;
using CustomIdentity.Models;
using CustomIdentity.Services;


using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using LoginRequest = CustomIdentity.Models.LoginRequest;
using RegisterRequest = CustomIdentity.Models.RegisterRequest;




namespace CustomIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserSevices _userSevices;

        public AuthController(IUserSevices userSevices)
        {
            _userSevices = userSevices;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var addUserDto = new AddUserDto
            {
                Email = request.Email,
                Password = request.Password,

            };

            var result = await _userSevices.AddUser(addUserDto);

            if (result.IsSucceed)
                return Ok(result.Message);
            else
                return BadRequest(result.Message);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {

            var LoginUserDto = new LoginUserDto
            {
                Email = request.Email,
                Password = request.Password,
            };

            var result = await _userSevices.LoginUser(LoginUserDto);

            if (!result.IsSucceed)
                return BadRequest(result.Message);

            var user = result.Data;

            var configuration = HttpContext.RequestServices.GetService<IConfiguration>();

            var token = JwtHelper.GenerateJwt(new JwtDto
            {
                Id = user.Id,
                Email = user.Email,
                UserType = user.UserType,
                SecretKey = configuration["Jwt:SecretKey"]!,
                Issuer = configuration["Jwt:Issuer"]!,
                Audience = configuration["Jwt:Audience"]!,
                ExpireMinutes = int.Parse(configuration["Jwt:ExpireMinutes"]!)
            });


            return Ok(new LoginResponse
            {
                Message = "Giriş başarıyla gerçekleşti",
                Token = token

            });
        }


    }
      


    
}
