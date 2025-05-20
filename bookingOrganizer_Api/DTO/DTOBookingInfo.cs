using bookingOrganizer_Api.Models;

namespace bookingOrganizer_Api.DTO
{
    public class DTOBookingInfo
    {
        public int BookingId { get; set; }

        public int GroupId { get; set; }

        public int TypeBookingId { get; set; }

        public DateTime Date { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Address { get; set; }

        public string PurchaseMethod { get; set; }

        public string SeatNumber { get; set; }

        public string Notes { get; set; }

        public virtual DTOGroup Group { get; set; }

        public virtual DTOTypeBooking TypeBooking { get; set; }
    }
}
