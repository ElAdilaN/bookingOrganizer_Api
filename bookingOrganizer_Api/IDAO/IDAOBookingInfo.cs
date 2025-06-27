using bookingOrganizer_Api.Models;

namespace bookingOrganizer_Api.IDAO
{
    public interface IDAOBookingInfo
    {
        public BookingInfo GetBookingInfoById(int id);
        public ICollection<BookingInfo> GetBookings(
            int? bookingId = null,
            int? groupId = null,
            int? typeBookingId = null,
            DateTime? date = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            string? address = null,
            string? purchaseMethod = null,
            string? seatNumber = null,
            string? notes = null);
        public Task  AddBooking(BookingInfo booking);
        public Task RemoveBooking(int bookingId);
        public Task UpdateBooking(BookingInfo booking);
    }

}
