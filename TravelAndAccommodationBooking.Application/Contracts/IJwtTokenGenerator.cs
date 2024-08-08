using System.Security.Claims;
using TravelAndAccommodationBooking.Domain.Entities;

namespace TravelAndAccommodationBooking.Application.Contracts
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}

