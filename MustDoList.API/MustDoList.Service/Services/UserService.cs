using AutoMapper;
using Microsoft.Extensions.Configuration;
using MustDoList.Config.Exceptions;
using MustDoList.Data.Repositories;
using MustDoList.Dto.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MustDoList.Service.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IConfiguration configuration, IActiveUserService activeUserService, IUserRepository userRepository, IMapper mapper) : base(configuration, activeUserService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserAuthenticationDTO> Authenticate(string email, string password)
        {
            var user = await _userRepository.Authenticate(email, password);

            if (user == null) throw new MustDoListException("User not found.");

            return _mapper.Map<UserAuthenticationDTO>(user);
        }

        public async Task<string> RetrieveRefreshToken(string email)
        {
            var user = await _userRepository.FindByEmail(email);
            return user.RefreshToken;
        }

        public async Task<bool> RevokeRefreshToken(string email)
        {
            return await SaveRefreshToken(email, "");
        }

        public async Task<bool> SaveRefreshToken(string email, string refreshToken)
        {
            var user = await _userRepository.FindByEmail(email);
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                await _userRepository.Save(user);

                return true;
            }

            return false;
        }
    }

    public interface IUserService
    {
        Task<UserAuthenticationDTO> Authenticate(string email, string password);
        Task<string> RetrieveRefreshToken(string email);
        Task<bool> RevokeRefreshToken(string email);
        Task<bool> SaveRefreshToken(string email, string refreshToken);
    }
}
