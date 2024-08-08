using System.Security.Claims;
using MediatR;
using TravelAndAccommodationBooking.Application.Commands.UserCommands;
using TravelAndAccommodationBooking.Application.Contracts;
using TravelAndAccommodationBooking.Application.DTOs.UserDTOs.Responses;
using TravelAndAccommodationBooking.Application.Interfaces;

namespace TravelAndAccommodationBooking.Application.Handlers.LoginHandler
{
    public sealed class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, TokenResponse>
    {
        private readonly IUserRepostiory _userRepo;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public RefreshTokenCommandHandler(IUserRepostiory userRepo, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepo = userRepo;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<TokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var principal = _jwtTokenGenerator.GetPrincipalFromExpiredToken(request.refreshTokenDTO.Token);
            if (principal == null)
            {
                return new TokenResponse(null, null, "Invalid token");
            }

            var email = principal.FindFirstValue(ClaimTypes.Email);
            if (email == null)
            {
                return new TokenResponse(null, null, "Invalid token");
            }

            var user = await _userRepo.GetUserByEmailAsync(email);
            if (user == null)
            {
                return new TokenResponse(null, null, "User not found");
            }

            var savedRefreshToken = await _userRepo.GetRefreshTokenAsync(user.Id);
            if (savedRefreshToken != request.refreshTokenDTO.RefreshToken)
            {
                return new TokenResponse(null, null, "Invalid refresh token");
            }

            var newJwtToken = _jwtTokenGenerator.GenerateToken(user);
            var newRefreshToken = _jwtTokenGenerator.GenerateRefreshToken();

            await _userRepo.SaveRefreshTokenAsync(user.Id, newRefreshToken, DateTime.UtcNow.AddMinutes(60));

            return new TokenResponse(newJwtToken, newRefreshToken);
        }
    }
}
