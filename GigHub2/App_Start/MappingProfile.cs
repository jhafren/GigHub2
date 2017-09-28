using AutoMapper;
using GigHub2.Controllers.Api;
using GigHub2.Dtos;
using GigHub2.Models;

namespace GigHub2.App_Start
{
    public class MappingProfile : Profile
    {
        public static void Initialize()
        {
            Mapper.Initialize(c => c.CreateMap<Genre, GenreDto>());
            Mapper.Initialize(c => c.CreateMap<ApplicationUser, UserDto>());
            Mapper.Initialize(c => c.CreateMap<Gig, GigDto>());
            Mapper.Initialize(c => c.CreateMap<Notification, NotificationDto>());
        }
    }
}