using AutoMapper;
using Microsoft.Extensions.Configuration;
using MustDoList.Data.Models;
using MustDoList.Data.Repositories;
using MustDoList.Dto.Commons;
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

        public async Task<ListBase<TodoDTO>> GetList(int pageNumber, int pageSize)
        {
            var activeUser = await _activeUserService.GetUser();
            var todos = await _todoRepository.GetByUser(pageNumber, pageSize, activeUser);
            var dtoItems = _mapper.Map<List<TodoDTO>>(todos);
            
            var ret = new ListBase<TodoDTO>();
            ret.Items = dtoItems;
            ret.ItemsQty = await _todoRepository.GetCountByUser(activeUser);

            return ret;
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
        Task<ListBase<TodoDTO>> GetList(int pageNumber, int pageSize);
    }
}
