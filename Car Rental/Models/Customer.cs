namespace CarRental.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }  // Primary Key
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNum { get; set; }

        // Navigation property for the related Reservation entity
        public ICollection<Reservation> Reservations { get; set; }
    }
}
