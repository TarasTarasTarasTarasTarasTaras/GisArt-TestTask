using PassengerTimeTracker.Entities.DTOs;
using PassengerTimeTracker.Entities.Filters;

namespace PassengerTimeTracker.Data
{
    public interface ITripRepository
    {
        IEnumerable<TripDTO> GetAllTrips(TripFilterModel? filters = null);
    }
}