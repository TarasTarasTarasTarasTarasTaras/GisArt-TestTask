using PassengerTimeTracker.Entities.DTOs;
using PassengerTimeTracker.Entities.Filters;

namespace PassengerTimeTracker.Data
{
    public interface IDriverRepository
    {
        IEnumerable<DriverDTO> GetAllDrivers(DriverFilterModel filters);
    }
}