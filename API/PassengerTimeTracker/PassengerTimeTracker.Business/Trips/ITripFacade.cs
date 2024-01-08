using PassengerTimeTracker.Entities.DTOs;
using PassengerTimeTracker.Entities.Filters;

namespace PassengerTimeTracker.Business.TripFacade
{
    public interface ITripFacade
    {
        IEnumerable<TripDTO> GetAllTrips(TripFilterModel filters);
    }
}