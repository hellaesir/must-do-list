using AutoMapper;
using Microsoft.Extensions.Configuration;
using MustDoList.Data.Models;
using MustDoList.Data.Repositories;
using MustDoList.Dto.Todo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MustDoList.Service.Services
{
    public class TodoService : BaseService, ITodoService
    {
        private readonly ITodoRepository _todoRepository;

        public TodoService(IConfiguration configuration, IActiveUserService activeUserService, IMapper mapper, ITodoRepository todoRepository) : base(configuration, activeUserService, mapper)
        {
            _todoRepository = todoRepository;
        }

        public async Task<List<TodoDTO>> GetByUser()
        {
            var activeUser = await _activeUserService.GetUser();
            var todos = await _todoRepository.GetByUser(activeUser);

            return _mapper.Map<List<TodoDTO>>(todos);
        }

        public async Task<bool> Save(TodoDTO todo)
        {
            var activeUser = await _activeUserService.GetUser();
            var todoEntity = _mapper.Map<Todo>(todo);
            return await _todoRepository.Save(todoEntity, activeUser);
        }
    }

    public interface ITodoService
    {
        Task<bool> Save(TodoDTO todo);
        Task<List<TodoDTO>> GetByUser();
    }
}
