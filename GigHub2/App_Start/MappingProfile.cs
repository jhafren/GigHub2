using AutoMapper;
using GigHub2.Controllers.Api;
using GigHub2.Dtos;
using GigHub2.Models;

namespace GigHub2.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.Initialize(c => c.CreateMap<ApplicationUser, UserDto>());
            Mapper.Initialize(c => c.CreateMap<Gig, GenreDto>());
            Mapper.Initialize(c => c.CreateMap<Notification, NotificationDto>());
        }
    }
}