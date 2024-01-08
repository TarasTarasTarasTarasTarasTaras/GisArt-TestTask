using Microsoft.EntityFrameworkCore;
using PassengerTimeTracker.Data.EF.Entities;

namespace PassengerTimeTracker.Data.EF.Context
{
    public class PassengerTimeTrackerContext : DbContext
    {
        public PassengerTimeTrackerContext()
        {
        }

        public PassengerTimeTrackerContext(DbContextOptions<PassengerTimeTrackerContext> options)
            : base(options)
        {
        }

        public DbSet<Trip> Trips { get; set; }
    }
}
