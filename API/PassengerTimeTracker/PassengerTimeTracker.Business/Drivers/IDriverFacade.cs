using PassengerTimeTracker.Entities.DTOs;
using PassengerTimeTracker.Entities.Filters;

namespace PassengerTimeTracker.Business.Drivers
{
    public interface IDriverFacade
    {
        IEnumerable<DriverDTO> GetAllDrivers(DriverFilterModel filters);
    }
}