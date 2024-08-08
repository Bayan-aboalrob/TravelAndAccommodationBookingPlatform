using TravelAndAccommodationBooking.Application.Commands.UserCommands;
using TravelAndAccommodationBooking.Application.DTOs.UserDTOs.Requests;
using TravelAndAccommodationBooking.Application.DTOs.UserDTOs.Responses;

namespace TravelAndAccommodationBooking.Application.Contracts
{
    public interface IAuthenticationService
    {
        Task<LoginResponse> LoginAsync(LoginDTO loginDto, CancellationToken cancellationToken);
        Task<TokenResponse> RefreshTokenAsync(RefreshTokenDTO refreshTokenDto);
    }

}
