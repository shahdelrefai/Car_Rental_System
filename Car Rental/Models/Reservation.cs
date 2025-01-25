namespace CarRental.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }  // Primary Key
        public int CustomerId { get; set; }
        public string PlateId { get; set; }
        public int OfficeId { get; set; }
        public int Payment { get; set; }
        public int? Cost { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        // Foreign Keys
        public Customer Customer { get; set; }
        public Car Car { get; set; }
        public Office Office { get; set; }
    }
}
