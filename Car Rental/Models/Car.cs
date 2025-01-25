namespace CarRental.Models
{
    public class Car
    {
        public string PlateId { get; set; }  // Primary Key
        public string Model { get; set; }
        public int Year { get; set; }
        public int Status { get; set; }

        // Foreign Key
        public int OfficeId { get; set; }
        public int? CustomerId { get; set; } = null;

        // Navigation property for the related Office and Reservation entities
        public Office Office { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
