using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MustDoList.API.Security.JWT;
using MustDoList.Config.Exceptions;
using MustDoList.Dto.Commons;
using MustDoList.Dto.Todo;
using MustDoList.Dto.User;
using MustDoList.Service.Services;

namespace MustDoList.API.Controllers
{
    [Route("api/todo")]
    [ApiController]
    public class TodoController : BaseController
    {
        private readonly ITodoService _todoService;

        public TodoController(IConfiguration configuration, IActiveUserService activeUserService, ITodoService? todoService) : base(configuration, activeUserService)
        {
            _todoService = todoService;
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<ActionResult<TodoDTO>> Create([FromBody] TodoDTO todo)
        {
            try
            {
                if (await _todoService.Save(todo))
                    return Ok(todo);

                return NoContent();
            }
            catch (MustDoListException ex)
            {
                return BadRequest(new ErrorResponse(ex));
            }
            catch (Exception ex)
            {
                return Problem(ex.ToString());
            }
        }

        [HttpPost("list")]
        [Authorize]
        public async Task<ActionResult<ListBase<TodoDTO>>> List(int pageNumber, int pageSize)
        {
            try
            {
                ListBase<TodoDTO> lista = await _todoService.GetList(pageNumber, pageSize);

                return Ok(lista);
            }
            catch (MustDoListException ex)
            {
                return BadRequest(new ErrorResponse(ex));
            }
            catch (Exception ex)
            {
                return Problem(ex.ToString());
            }
        }
    }
}
