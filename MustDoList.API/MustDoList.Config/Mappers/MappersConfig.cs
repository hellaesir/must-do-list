using AutoMapper;
using MustDoList.Data.Models;
using MustDoList.Dto.Todo;
using MustDoList.Dto.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MustDoList.Config.Mappers
{
    public class MappersConfig : Profile
    {
        public MappersConfig()
        {
            CreateMap<User, UserAuthenticationDTO>().ReverseMap();
            CreateMap<User, LoginRequestDTO>().ReverseMap();
            CreateMap<User, AuthResponseDTO>().ReverseMap();
            CreateMap<User, ActiveUserDTO>().ReverseMap();

            CreateMap<Todo, TodoDTO>().ReverseMap();
        }
    }
}
