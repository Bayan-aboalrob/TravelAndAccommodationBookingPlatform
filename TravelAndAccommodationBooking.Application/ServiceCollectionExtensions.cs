using TravelAndAccommodationBooking.Application.Profiles;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using TravelAndAccommodationBooking.Application.Validators.UserValidators;
using TravelAndAccommodationBooking.Application.Services;
using TravelAndAccommodationBooking.Application.Contracts;

namespace TravelAndAccommodationBooking.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = typeof(ServiceCollectionExtensions).Assembly;

        services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssembly(assembly));
        services.AddAutoMapper(typeof(UserProfile));

        services.AddValidatorsFromAssemblyContaining<LoginDTOValidator>();
        services.AddValidatorsFromAssemblyContaining<RegisterUserDTOValidator>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        return services;
    }
}