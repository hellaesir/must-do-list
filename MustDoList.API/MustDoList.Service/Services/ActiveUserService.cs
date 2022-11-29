using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MustDoList.Data.Repositories;
using MustDoList.Dto.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MustDoList.Service.Services
{
    public class ActiveUserService : IActiveUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ActiveUserService(IConfiguration configuration, IMapper mapper, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)  
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ActiveUserDTO> GetSystemUser()
        {
            var user = await _userRepository.FindById(1);

            return _mapper.Map<ActiveUserDTO>(user);
        }

        public async Task<ActiveUserDTO> GetUser()
        {
            if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User != null && _httpContextAccessor.HttpContext.User.Identity != null)
            {
                var user = await _userRepository.FindById(int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name));

                return _mapper.Map<ActiveUserDTO>(user);
            }

            return null;
        }
    }

    public interface IActiveUserService
    {
        Task<ActiveUserDTO> GetUser();
        Task<ActiveUserDTO> GetSystemUser();
    }
}
