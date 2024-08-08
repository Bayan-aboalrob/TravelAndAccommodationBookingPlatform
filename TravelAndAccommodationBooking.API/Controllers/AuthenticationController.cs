using Microsoft.AspNetCore.Mvc;
using TravelAndAccommodationBooking.Application.Contracts;
using TravelAndAccommodationBooking.Application.DTOs.UserDTOs.Requests;

namespace TravelAndAccommodationBooking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO, CancellationToken cancellationToken)
        {
            var response = await _authenticationService.LoginAsync(loginDTO, cancellationToken);
            if (response.IsLogedin)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDTO refreshTokenDTO)
        {
            var response = await _authenticationService.RefreshTokenAsync(refreshTokenDTO);
            return Ok(response);
        }
    }

}
