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
            return await _context.Users.FirstOrDefaultAsync(f => f.Email == email && f.Password == password);
        }

        public async Task<User> FindByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(f => f.Email == email);
        }

        public async Task<User> GetById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task Save(User user)
        {
            if(user.Id == 0)
                _context.Users.Add(user);
            else
            {
                var entity = await _context.Users.FirstOrDefaultAsync(f => f.Id == user.Id);
                entity = user;
            }

            await _context.SaveChangesAsync();
        }
    }

    public interface IUserRepository
    {
        Task<User> GetById(int id);
        Task<User> Authenticate(string email, string password);
        Task<User> FindByEmail(string email);
        Task Save(User user);
    }
}
