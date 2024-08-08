using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TravelAndAccommodationBooking.Application.Interfaces;
using TravelAndAccommodationBooking.Domain.Entities;
using TravelAndAccommodationBooking.Infrastructure.Persistence.DBContext;

namespace TravelAndAccommodationBooking.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepostiory
    {
        private readonly AccomodationAndTravelDbContext _context;
        private readonly IConfiguration configuration;

        public UserRepository(AccomodationAndTravelDbContext travelAndAccomodationDbContext,IConfiguration configuration)
        {
            this._context = travelAndAccomodationDbContext;
            this.configuration = configuration;
        }
        public async Task<User> RegisterUserAsync(User user)
        {
            await _context.Users.AddAsync(user); 
            await _context.SaveChangesAsync(); 
            return user;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
           return await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
        }
        public async Task SaveRefreshTokenAsync(Guid userId, string refreshToken, DateTime expiryTime)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                throw new ArgumentException("User not found");

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = expiryTime;
            await _context.SaveChangesAsync();
        }

        public async Task<string> GetRefreshTokenAsync(Guid userId)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId);
            return user?.RefreshToken;
        }

        public async Task RemoveRefreshTokenAsync(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                throw new ArgumentException("User not found");

            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null; // Set to null for nullable DateTime
            await _context.SaveChangesAsync();
        }
    }
}
