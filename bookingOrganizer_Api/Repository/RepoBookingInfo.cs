using bookingOrganizer_Api.DTO;
using bookingOrganizer_Api.Models;

namespace bookingOrganizer_Api.Repository
{
    public interface RepoBookingInfo
    {
        public DTOBookingInfo getBookingInfoById(int id);
        public ICollection<DTOBookingInfo> getBookings(
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
        public void AddBooking(DTOBookingInfo booking);
        public void RemoveBooking(int bookingId);

        public Task UpdateBooking(DTOBookingInfo dtoBooking);

    }
}
