using TravelAndAccommodationBooking.Domain.Enums;
namespace TravelAndAccommodationBooking.Domain.Entities
{
    public class User: Person
    {
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
