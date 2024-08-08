using Microsoft.EntityFrameworkCore;
using TravelAndAccommodationBooking.Domain.Entities;

namespace TravelAndAccommodationBooking.Infrastructure.Persistence.DBContext
{
    public class AccomodationAndTravelDbContext : DbContext
    {
        public AccomodationAndTravelDbContext(DbContextOptions<AccomodationAndTravelDbContext> options): base(options) 
        { 

        }
        public DbSet<User> Users { get; set; }
       
    }
}
