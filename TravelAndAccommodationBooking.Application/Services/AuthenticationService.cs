using TravelAndAccommodationBooking.Application.Commands.UserCommands;
using TravelAndAccommodationBooking.Application.Contracts;
using TravelAndAccommodationBooking.Application.DTOs.UserDTOs.Responses;
using MediatR;
using TravelAndAccommodationBooking.Application.DTOs.UserDTOs.Requests;

namespace TravelAndAccommodationBooking.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IMediator _mediator;

        public AuthenticationService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<LoginResponse> LoginAsync(LoginDTO loginDto, CancellationToken cancellationToken)
        {
            var command = new LoginCommand(loginDto);
            var response = await _mediator.Send(command, cancellationToken);
            return response;
        }

        public async Task<TokenResponse> RefreshTokenAsync(RefreshTokenDTO refreshTokenDto)
        {
            var command = new RefreshTokenCommand(refreshTokenDto);
            var response = await _mediator.Send(command);
            return response;
        }
    }
}
