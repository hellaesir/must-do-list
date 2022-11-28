using MustDoList.Dto.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MustDoList.Service.Services
{
    public class UserService : IUserService
    {
        public Task<UserAuthenticationDTO> Authenticate(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<string> RetrieveRefreshToken(string email)
        {
            throw new NotImplementedException();
        }

        public Task RevokeRefreshToken(string email)
        {
            throw new NotImplementedException();
        }

        public Task SaveRefreshToken(string email, string refreshToken)
        {
            throw new NotImplementedException();
        }
    }

    public interface IUserService
    {
        Task<UserAuthenticationDTO> Authenticate(string email, string password);
        Task<string> RetrieveRefreshToken(string email);
        Task RevokeRefreshToken(string email);
        Task SaveRefreshToken(string email, string refreshToken);
    }
}
