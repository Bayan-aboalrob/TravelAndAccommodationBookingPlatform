using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using TravelAndAccommodationBooking.Application.Commands.UserCommands;
using TravelAndAccommodationBooking.Application.Contracts;
using TravelAndAccommodationBooking.Application.DTOs.UserDTOs.Requests;
using TravelAndAccommodationBooking.Application.DTOs.UserDTOs.Responses;
using TravelAndAccommodationBooking.Application.Exceptions.UserExceptions;
using TravelAndAccommodationBooking.Application.Interfaces;
using TravelAndAccommodationBooking.Domain.Entities;

namespace TravelAndAccommodationBooking.Application.Handlers.UserHandlers
{
    public sealed class RegisterUserCommandHandler : IRequestHandler<CreateUserCommand,RegisrationResponse>
    {
        private readonly IUserRepostiory _userRepo;
        private readonly IMapper _mapper;
        private readonly IValidator<RegisterUserDTO> _validator;

        public RegisterUserCommandHandler(IUserRepostiory userRepo, IMapper mapper, IValidator<RegisterUserDTO> validator)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<RegisrationResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(request.registerUserDTO, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage));
                return new RegisrationResponse(false, $"Validation failed: {errors}");
            }

            var newUser = _mapper.Map<User>(request.registerUserDTO);
            var fetchUser = await _userRepo.GetUserByEmailAsync(newUser.Email);

            if (fetchUser != null) throw new UserAlreadyExistsException($"User with email {request.registerUserDTO.Email} already exists."); ;

            newUser.Password = BCrypt.Net.BCrypt.HashPassword(request.registerUserDTO.Password);

             await _userRepo.RegisterUserAsync(newUser);

            return new RegisrationResponse(true, "Registration is successfully completed !");

        }
    }
}
