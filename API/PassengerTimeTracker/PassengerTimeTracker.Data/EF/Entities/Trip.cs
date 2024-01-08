using System.ComponentModel.DataAnnotations.Schema;

namespace PassengerTimeTracker.Data.EF.Entities
{
    [Table("trip")]
    public class Trip
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("driver_id")]
        public int DriverId { get; set; }

        [Column("pickup")]
        public DateTime PickUp { get; set; }

        [Column("dropoff")]
        public DateTime DropOff { get; set; }
    }
}
