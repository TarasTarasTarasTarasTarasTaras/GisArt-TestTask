using PassengerTimeTracker.Data;
using PassengerTimeTracker.Entities.DTOs;
using PassengerTimeTracker.Entities.Filters;

namespace PassengerTimeTracker.Business.TripFacade
{
    public class TripFacade : ITripFacade
    {
        private readonly ITripRepository _tripRepository;

        public TripFacade(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        public IEnumerable<TripDTO> GetAllTrips(TripFilterModel filters)
        {
            return _tripRepository.GetAllTrips(filters);
        }
    }
}
