using MediatR;
using TravelAndAccommodationBooking.Application.DTOs.UserDTOs.Requests;
using TravelAndAccommodationBooking.Application.DTOs.UserDTOs.Responses;

namespace TravelAndAccommodationBooking.Application.Commands.UserCommands
{
    public record CreateUserCommand(RegisterUserDTO registerUserDTO) : IRequest<RegisrationResponse>;
}
