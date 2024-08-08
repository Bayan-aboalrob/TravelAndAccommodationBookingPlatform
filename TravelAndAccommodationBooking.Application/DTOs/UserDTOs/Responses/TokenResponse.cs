namespace TravelAndAccommodationBooking.Application.DTOs.UserDTOs.Responses
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string Message { get; set; }

        public TokenResponse(string token, string refreshToken, string message = null, bool success = true)
        {
            Token = token;
            RefreshToken = refreshToken;
            Message = message;
        }
    }
}
