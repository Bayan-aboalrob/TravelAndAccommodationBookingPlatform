using System.ComponentModel.DataAnnotations;
using TravelAndAccommodationBooking.Domain.Enums;
namespace TravelAndAccommodationBooking.Application.DTOs.UserDTOs.Requests
{
    public class RegisterUserDTO
    {
        [Required]
        public string? FirstName { get; set; } = string.Empty;
        [Required]
        public string? LastName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string? Email { get; set; } = string.Empty;

        [Required]
        public string? Password { get; set; } = string.Empty;

        [Required, Compare(nameof(Password))]
        public string? ConfirmedPassword { get; set; } = string.Empty;

        [Required]
        public UserRole Role { get; set; } = UserRole.Customer;

    }

}
