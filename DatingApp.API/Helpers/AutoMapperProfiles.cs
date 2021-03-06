using System.Linq;
using AutoMapper;
using DatingApp.API.Dtos;
using DatingApp.API.Models;

namespace DatingApp.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
           CreateMap<User, USerListDTO>()
                .ForMember(dest => dest.PhotoUrl, opt => {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
                })
                .ForMember(dest => dest.Age, opt => {
                    opt.MapFrom(d => d.DateOfBirth.CalculateAge());
                });
            CreateMap<User, UserDetailDTO>()
                .ForMember(dest => dest.PhotoUrl, opt => {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
                })
                .ForMember(dest => dest.Age, opt => {
                    opt.MapFrom(d => d.DateOfBirth.CalculateAge());
                });
            CreateMap<Photo, UserDetailDTO>();
            
            CreateMap<Photo,PhotoForDetailDTO>();
            CreateMap<UserUpdateDTO,User>();
            CreateMap<Photo,PhotoForReturnDto>();             
            CreateMap<AddPhotoDTO,Photo>();
            CreateMap<UserDTO,User>();

            CreateMap<AddMessageDTO,Message>().ReverseMap();
            CreateMap<Message,MessageToReturnDTO>()
               .ForMember(m=>m.SenderPhotoUrl , opt => opt
               .MapFrom(u => u.Sender.Photos.FirstOrDefault(p => p.IsMain).Url))               
               .ForMember(m=>m.RecipientPhotoUrl , opt => opt.
               MapFrom(u => u.Recipient.Photos.FirstOrDefault(p => p.IsMain).Url));
        
        }
       
    }
}