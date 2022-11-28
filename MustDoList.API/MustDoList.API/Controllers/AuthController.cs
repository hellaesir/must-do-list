using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MustDoList.API.Security.JWT;
using MustDoList.Dto.User;
using MustDoList.Service.Services;

namespace MustDoList.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IUserService _userService;

        public AuthController(IConfiguration configuration, IActiveUserService activeUserService, IUserService userService) : base(configuration, activeUserService)
        {
            _userService = userService;
        }


        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDTO>> Login([FromBody] LoginRequestDTO login)
        {
            // Recupera o usuário
            UserAuthenticationDTO user = await _userService.Authenticate(login.Email, login.Password);

            // Verifica se o usuário existe
            if (user == null)
                return NotFound(new { message = "Invalid user or password" });

            // Gera o Token
            var token = TokenService.GenerateToken(_configuration, user);
            var refreshToken = TokenService.GenerateRefreshToken();
            await TokenService.SaveRefreshToken(_userService, user.Email, refreshToken);

            // Retorna os dados
            return new AuthResponseDTO()
            {
                AccessToken = token,
                RefreshToken = refreshToken,
            };
        }
    }
}
