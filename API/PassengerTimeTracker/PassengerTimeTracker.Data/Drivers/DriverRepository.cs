using PassengerTimeTracker.Data.EF.Context;
using PassengerTimeTracker.Entities.DTOs;
using PassengerTimeTracker.Entities.Filters;

namespace PassengerTimeTracker.Data
{
    public class DriverRepository : IDriverRepository
    {
        private readonly PassengerTimeTrackerContext _context;

        public DriverRepository(PassengerTimeTrackerContext context)
        {
            _context = context;
        }

        public IEnumerable<DriverDTO> GetAllDrivers(DriverFilterModel filters)
        {
            if (filters.DriverId != null)
            {
                int? driverId = _context.Trips.FirstOrDefault(x => x.DriverId == filters.DriverId)?.DriverId;
                return driverId == null ? Enumerable.Empty<DriverDTO>() : new List<DriverDTO>() { new() { DriverId = driverId.Value } };
            }

            return _context
                .Trips
                .OrderBy(t => t.DriverId)
                .Select(t => new DriverDTO() { DriverId = t.DriverId })
                .AsEnumerable()
                .DistinctBy(t => t.DriverId);
        }
    }
}
