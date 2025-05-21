using bookingOrganizer_Api.Models;

namespace bookingOrganizer_Api.IDAO
{
    public interface IDAOBookingInfo
    {
        public BookingInfo getBookingInfoById(int id);
        public IEnumerable<BookingInfo> getBookings(
          int? bookingId = null,
          int? groupId = null,
          int? typeBookingId = null,
          DateTime? date = null,
          DateTime? startDate = null,
          DateTime? endDate = null,
          string address = null,
          string purchaseMethod = null,
          string seatNumber = null,
          string notes = null);
        public void AddBooking(BookingInfo booking);
        public void RemoveBooking(int bookingId);
        public void UpdateBooking(BookingInfo booking);

    }
}
