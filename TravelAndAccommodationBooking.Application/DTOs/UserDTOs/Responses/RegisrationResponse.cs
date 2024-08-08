namespace TravelAndAccommodationBooking.Application.DTOs.UserDTOs.Responses
{
    public record RegisrationResponse(bool IsCreated, string Message = null!);
}
