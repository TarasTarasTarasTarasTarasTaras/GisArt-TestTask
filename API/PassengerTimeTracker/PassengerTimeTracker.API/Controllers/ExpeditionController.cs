using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PassengerTimeTracker.Business.Drivers;
using PassengerTimeTracker.Business.TripFacade;
using PassengerTimeTracker.Entities.DTOs;
using PassengerTimeTracker.Entities.Filters;

namespace PassengerTimeTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpeditionController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private readonly ITripFacade _tripFacade;
        private readonly IDriverFacade _driverFacade;

        public ExpeditionController(IMemoryCache cache, ITripFacade tripFacade, IDriverFacade driverFacade)
        {
            _cache = cache;
            _tripFacade = tripFacade;
            _driverFacade = driverFacade;
        }

        [HttpGet("trips")]
        public IEnumerable<TripDTO> GetAllTrips([FromQuery] TripFilterModel filters)
        {
            IEnumerable<TripDTO> allTrips = _tripFacade.GetAllTrips(filters);
            return allTrips;
        }

        [HttpGet("drivers")]
        public IEnumerable<DriverDTO> GetAllDrivers([FromQuery] DriverFilterModel filters)
        {
            if (!_cache.TryGetValue("Drivers", out IEnumerable<DriverDTO>? allDrivers))
            {
                allDrivers = _driverFacade.GetAllDrivers(filters);
                if (filters.CalculatePayableTime)
                {
                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(1));
                    _cache.Set("Drivers", allDrivers, cacheEntryOptions);
                }
            }

            return allDrivers!;
        }
    }
}
