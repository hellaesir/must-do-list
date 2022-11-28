using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MustDoList.Service.Services
{
    public class BaseService
    {
        private readonly IConfiguration _configuration;
        private readonly IActiveUserService _activeUserService;

        public BaseService(IConfiguration configuration, IActiveUserService activeUserService)
        {
            _configuration = configuration;
            _activeUserService = activeUserService;
        }
    }
}
