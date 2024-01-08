namespace PassengerTimeTracker.Entities.DTOs
{
    public class TripDTO
    {
        public int Id { get; set; }
        public int DriverId { get; set; }
        public DateTime PickUp { get; set; }
        public DateTime DropOff { get; set; }
    }
}
