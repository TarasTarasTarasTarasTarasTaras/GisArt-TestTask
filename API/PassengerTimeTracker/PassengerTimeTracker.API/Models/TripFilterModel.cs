namespace PassengerTimeTracker.API.Models
{
    public class TripFilterModel
    {
        public string? Id { get; set; }
        public string? DriverId { get; set; }
        public string? PickUp { get; set; }
        public string? DropOff { get; set; }
    }
}
