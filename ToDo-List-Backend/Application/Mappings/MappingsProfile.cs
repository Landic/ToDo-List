using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Task;
using Application.DTOs.User;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile() 
        {
            CreateMap<User, UserDto>();
            CreateMap<UserCreateDto, User>()
                .ForMember(dest => dest.Password_hash, opt => opt.MapFrom(src => src.Password));

            CreateMap<ToDoTask, TaskDto>();
            CreateMap<TaskCreateDto, ToDoTask>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<TaskUpdateDto, ToDoTask>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
