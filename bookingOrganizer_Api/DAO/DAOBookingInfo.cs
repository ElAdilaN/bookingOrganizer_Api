using bookingOrganizer_Api.DTO;
using bookingOrganizer_Api.IDAO;
using bookingOrganizer_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace bookingOrganizer_Api.DAO
{
    public class DAOBookingInfo : IDAOBookingInfo
    {
        public BookingInfo getBookingInfoById(int id)
        {
            using (var context = new BookingContext())
            {
                return context.BookingInfos.Where(b => b.BookingId == id).FirstOrDefault();
            }
        }

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
               string notes = null)
        {
            using (var _context = new BookingContext())
            {
                var query = _context.BookingInfos.AsQueryable();

                if (bookingId.HasValue)
                    query = query.Where(b => b.BookingId == bookingId.Value);

                if (groupId.HasValue)
                    query = query.Where(b => b.GroupId == groupId.Value);

                if (typeBookingId.HasValue)
                    query = query.Where(b => b.TypeBookingId == typeBookingId.Value);

                if (date.HasValue)
                    query = query.Where(b => b.Date.Date == date.Value.Date);

                if (startDate.HasValue)
                    query = query.Where(b => b.StartDate >= startDate.Value);

                if (endDate.HasValue)
                    query = query.Where(b => b.EndDate <= endDate.Value);

                if (!string.IsNullOrWhiteSpace(address))
                    query = query.Where(b => b.Address.Contains(address));

                if (!string.IsNullOrWhiteSpace(purchaseMethod))
                    query = query.Where(b => b.PurchaseMethod.Contains(purchaseMethod));

                if (!string.IsNullOrWhiteSpace(seatNumber))
                    query = query.Where(b => b.SeatNumber.Contains(seatNumber));

                if (!string.IsNullOrWhiteSpace(notes))
                    query = query.Where(b => b.Notes.Contains(notes));

                return query.ToList();
            }
        }

        public  void AddBooking(BookingInfo booking)
        {
            using (var _context = new BookingContext())
            {
                _context.BookingInfos.Add(booking);
                _context.SaveChanges();
            }
        }

        public  void RemoveBooking(int bookingId)
        {
            using (var _context = new BookingContext())
            {
                var booking = _context.BookingInfos.FirstOrDefault(b => b.BookingId == bookingId);
                if (booking != null)
                {
                    _context.BookingInfos.Remove(booking);
                    _context.SaveChanges();
                }
            }
        }
        public  async void UpdateBooking(BookingInfo booking)
        {
            using (var _context = new BookingContext())
            {
                var existingBooking = await _context.BookingInfos.FirstOrDefaultAsync(b => b.BookingId == booking.BookingId);

                if (existingBooking == null)
                    throw new KeyNotFoundException($"Booking with Id {booking.BookingId} not found.");

                // Update fields
                existingBooking.GroupId = booking.GroupId;
                existingBooking.TypeBookingId = booking.TypeBookingId;
                existingBooking.Date = booking.Date;
                existingBooking.StartDate = booking.StartDate;
                existingBooking.EndDate = booking.EndDate;
                existingBooking.Address = booking.Address;
                existingBooking.PurchaseMethod = booking.PurchaseMethod;
                existingBooking.SeatNumber = booking.SeatNumber;
                existingBooking.Notes = booking.Notes;

                await _context.SaveChangesAsync();

            }
        }
    }
}