using MediatR;
using Microsoft.AspNetCore.Mvc;
using TravelAndAccommodationBooking.Application.DTOs.UserDTOs.Requests;
using TravelAndAccommodationBooking.Application.DTOs.UserDTOs.Responses;
using TravelAndAccommodationBooking.Application.Commands.UserCommands;
using TravelAndAccommodationBooking.Application.Exceptions.UserExceptions;
using TravelAndAccommodationBooking.Application.Interfaces;

namespace TravelAndAccommodationBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepostiory _user;
        private IMediator _mediator;
        public UserController(IUserRepostiory user, IMediator mediator) 
        {
            this._user = user;
            this._mediator = mediator;
        }
      
        [HttpPost("Register")]
        public async Task<ActionResult<RegisrationResponse>> RegisterUser(RegisterUserDTO registerUserDTO)
        {
            try
            {
                var command = new CreateUserCommand(registerUserDTO);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (UserAlreadyExistsException ex)
            {
                // Here we return  a 400 Bad Request with a meaningful message
                return BadRequest(new RegisrationResponse(false, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RegisrationResponse(false, "An unexpected error occurred."));
            }

        }
    }
}
