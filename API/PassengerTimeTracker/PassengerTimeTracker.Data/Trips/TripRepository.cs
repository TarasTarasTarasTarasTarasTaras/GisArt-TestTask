using AutoMapper;
using PassengerTimeTracker.Data.EF.Context;
using PassengerTimeTracker.Data.EF.Entities;
using PassengerTimeTracker.Entities.DTOs;
using PassengerTimeTracker.Entities.Filters;

namespace PassengerTimeTracker.Data
{
    public class TripRepository : ITripRepository
    {
        private readonly IMapper _mapper;
        private readonly PassengerTimeTrackerContext _context;

        public TripRepository(IMapper mapper, PassengerTimeTrackerContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public IEnumerable<TripDTO> GetAllTrips(TripFilterModel? filters = null)
        {
            IEnumerable<Trip> allTrips = _context.Trips;

            if (filters != null)
            {
                allTrips = allTrips.Where(t => (filters.Id == null || t.Id == filters.Id) &&
                            (filters.DriverId == null || t.DriverId == filters.DriverId) &&
                            (string.IsNullOrEmpty(filters.PickUp) || t.PickUp.ToString().Contains(filters.PickUp)) &&
                            (string.IsNullOrEmpty(filters.DropOff) || t.PickUp.ToString().Contains(filters.DropOff)));

                var propertyName = filters.Ordering.ToString();
                var propertyInfo = typeof(Trip).GetProperty(propertyName);

                if (propertyInfo != null)
                {
                    allTrips = allTrips.OrderBy(t => propertyInfo.GetValue(t, null));
                }
            }

            return _mapper.Map<IEnumerable<TripDTO>>(allTrips);
        }
    }
}
