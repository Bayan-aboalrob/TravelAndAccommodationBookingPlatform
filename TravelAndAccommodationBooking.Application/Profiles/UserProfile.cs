using AutoMapper;
using TravelAndAccommodationBooking.Application.DTOs.UserDTOs.Requests;
using TravelAndAccommodationBooking.Domain.Entities;

namespace TravelAndAccommodationBooking.Application.Profiles
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterUserDTO, User>()
               .ForMember(dest => dest.Password, opt => opt.Ignore());
            CreateMap<LoginDTO, User>();
        }
    }
}
