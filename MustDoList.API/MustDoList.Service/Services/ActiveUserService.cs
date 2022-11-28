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
        public Task<ActiveUserDTO> GetSystemUser()
        {
            throw new NotImplementedException();
        }

        public Task<ActiveUserDTO> GetUser()
        {
            throw new NotImplementedException();
        }
    }

    public interface IActiveUserService
    {
        Task<ActiveUserDTO> GetUser();
        Task<ActiveUserDTO> GetSystemUser();
    }
}
