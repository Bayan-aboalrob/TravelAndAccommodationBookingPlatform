namespace TravelAndAccommodationBooking.Application.DTOs.UserDTOs.Responses
{
    public record LoginResponse(bool IsLogedin, string Message=null!, string Token=null!,string RefreshToken=null!);
   
}
