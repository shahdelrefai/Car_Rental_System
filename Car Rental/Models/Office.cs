namespace CarRental.Models
{
    using System.Collections.Generic;
    using CarRental.Models;
    public class Office
    {
        public int OfficeId { get; set; }  // Primary Key
        public string Location { get; set; }
        public string PhoneNum { get; set; }

        // Navigation property for the related Car and Reservation entities
        public ICollection<Car> Cars { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
