using AutoMapper;
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
        internal readonly IActiveUserService _activeUserService;
        internal readonly IMapper _mapper;

        public BaseService(IConfiguration configuration, IActiveUserService activeUserService, IMapper mapper)
        {
            _configuration = configuration;
            _activeUserService = activeUserService;
            _mapper = mapper;
        }
    }
}
