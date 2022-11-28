using Microsoft.EntityFrameworkCore;
using MustDoList.Data.Context;
using MustDoList.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MustDoList.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MustDoListContext _context;

        public UserRepository(MustDoListContext context)
        {
            _context = context;
        }

        public async Task<User> Authenticate(string email, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(f => f.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase));
        }

        public async Task<User> GetById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(f => f.Id == id);
        }
    }

    public interface IUserRepository
    {
        Task<User> GetById(int id);
        Task<User> Authenticate(string email, string password);
    }
}
