using AutoMapper;
using GigHub.MVC.Core.DTOs;
using GigHub.MVC.Core.Models;

namespace GigHub.MVC.App_Start
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Gig, GigDTO>();
                cfg.CreateMap<ApplicationUser, ApplicationUserDTO>();
                cfg.CreateMap<Genre, GenreDTO>();
                cfg.CreateMap<Notification, NotificationDTO>();
            });
        }       
    }
}