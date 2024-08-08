using TravelAndAccommodationBooking.Domain.Entities;

namespace TravelAndAccommodationBooking.Application.Interfaces
{
    public interface IUserRepostiory
    {
        Task<User> RegisterUserAsync(User user);
        Task<User> GetUserByEmailAsync(string email);
        Task SaveRefreshTokenAsync(Guid userId, string refreshToken, DateTime expiryTime);
        Task<string> GetRefreshTokenAsync(Guid userId);
        Task RemoveRefreshTokenAsync(Guid userId);
    }
}
