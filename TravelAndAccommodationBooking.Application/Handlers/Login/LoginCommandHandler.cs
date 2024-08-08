using MediatR;
using TravelAndAccommodationBooking.Application.Commands.UserCommands;
using TravelAndAccommodationBooking.Application.DTOs.UserDTOs.Responses;
using TravelAndAccommodationBooking.Application.Contracts;
using AutoMapper;
using TravelAndAccommodationBooking.Domain.Entities;
using FluentValidation;
using TravelAndAccommodationBooking.Application.DTOs.UserDTOs.Requests;
using FluentValidation.Results;
using TravelAndAccommodationBooking.Application.Interfaces;


namespace TravelAndAccommodationBooking.Application.Handlers.LoginHandler
{
    public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IUserRepostiory _userRepo;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IMapper _mapper;
        private readonly IValidator<LoginDTO> _validator;
        public LoginCommandHandler(IUserRepostiory userRepo, IJwtTokenGenerator jwtTokenGenerator, IMapper mapper,IValidator<LoginDTO> validator)
        {
            _userRepo = userRepo;
            _jwtTokenGenerator=jwtTokenGenerator; 
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(request.loginDTO, cancellationToken);
            if (!validationResult.IsValid)
            {
                string errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                return new LoginResponse(false, errors);
            }

            var loginUser = _mapper.Map<User>(request.loginDTO);
            var fetchUser = await _userRepo.GetUserByEmailAsync(loginUser.Email);
            if (fetchUser == null) return new LoginResponse(false, "User not found");

            bool checkPassword = BCrypt.Net.BCrypt.Verify(request.loginDTO.Password, fetchUser.Password);
            if (!checkPassword) return new LoginResponse(false, "Invalid credentials");

            var token = _jwtTokenGenerator.GenerateToken(fetchUser);
            var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();

            await _userRepo.SaveRefreshTokenAsync(fetchUser.Id, refreshToken, DateTime.UtcNow.AddDays(7));

            return new LoginResponse(true, "Login successful", token, refreshToken);

        }
    }
}

