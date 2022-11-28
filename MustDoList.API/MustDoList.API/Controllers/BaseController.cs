using Microsoft.AspNetCore.Mvc;
using MustDoList.Dto.Commons;
using MustDoList.Dto.Configuration;
using MustDoList.Dto.User;
using MustDoList.Service.Services;

namespace MustDoList.API.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        public readonly IActiveUserService _activeUserService;
        public readonly AppSettings AppSettings;

        private ActiveUserDTO _activeUser;
        public ActiveUserDTO ActiveUser
        {
            get
            {
                if (_activeUser == null)
                {
                    var taskUser = _activeUserService.GetUser();
                    taskUser.Wait();
                    _activeUser = taskUser.Result;
                }

                return _activeUser;
            }
        }


        public BaseController(IConfiguration configuration, IActiveUserService activeUserService)
        {
            _configuration = configuration;
            _activeUserService = activeUserService;

            this.AppSettings = AppSettings.loadAppSettings(configuration);
        }
        protected IActionResult Error(string message, string detailed = null)
        {
            return new JsonResult(new ErrorResponse()
            {
                Message = message,
                Detailed = detailed
            });
        }
        protected IActionResult Error(Exception exception)
        {
            return new JsonResult(new ErrorResponse(exception))
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}
