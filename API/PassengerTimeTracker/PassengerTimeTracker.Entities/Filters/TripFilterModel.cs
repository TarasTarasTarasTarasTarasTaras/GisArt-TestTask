using PassengerTimeTracker.Entities.Enums;

namespace PassengerTimeTracker.Entities.Filters
{
    public class TripFilterModel
    {
        public int? Id { get; set; }
        public int? DriverId { get; set; }
        public string? PickUp { get; set; }
        public string? DropOff { get; set; }
        public TripSort Ordering { get; set; }
    }
}
