using Microsoft.EntityFrameworkCore;
using MustDoList.Data.Context;
using MustDoList.Data.Models;
using MustDoList.Dto.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MustDoList.Data.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly MustDoListContext _context;

        public TodoRepository(MustDoListContext context)
        {
            _context = context;
        }

        public async Task<bool> Save(Todo todo, ActiveUserDTO activeUser)
        {
            if (todo.Id == 0)
            {
                todo.UserId = activeUser.Id;
                _context.Todos.Add(todo);
            }
            else
            {
                var todoAux = await _context.Todos.Where(f => f.Id == todo.Id).FirstAsync();
                todoAux.Title = todo.Title;
                todoAux.Done = todo.Done;
            }

            return (await _context.SaveChangesAsync()) > 0;
        }

        public Task<List<Todo>> GetByUser(int pageNumber, int pageSize, ActiveUserDTO activeUser)
        {
            return _context.Todos.Where(f => f.UserId == activeUser.Id).Skip((pageNumber-1)*pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<bool> SetDone(int todoId)
        {
            var todo = await _context.Todos.FirstOrDefaultAsync(f => f.Id == todoId);

            if (todo == null) return false;
            if (todo.Done) return false;

            todo.Done = true;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SetUndone(int todoId)
        {
            var todo = await _context.Todos.FirstOrDefaultAsync(f => f.Id == todoId);

            if (todo == null) return false;
            if (!todo.Done) return false;

            todo.Done = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetCountByUser(ActiveUserDTO activeUser)
        {
            return await _context.Todos.CountAsync(f => f.UserId == activeUser.Id);
        }
    }

    public interface ITodoRepository
    {
        Task<List<Todo>> GetByUser(int pageNumber, int pageSize, ActiveUserDTO activeUser);
        Task<int> GetCountByUser(ActiveUserDTO activeUser);
        Task<bool> Save(Todo todo, ActiveUserDTO activeUser);
        Task<bool> SetDone(int todoId);
        Task<bool> SetUndone(int todoId);
    }
}
