using bookingOrganizer_Api.DTO;
using bookingOrganizer_Api.IDAO;
using bookingOrganizer_Api.Models;
using Microsoft.EntityFrameworkCore;


using bookingOrganizer_Api.Exceptions;
namespace bookingOrganizer_Api.DAO
{
    public class DAOBookingInfo : IDAOBookingInfo
    {
        public BookingInfo GetBookingInfoById(int id)
        {
            try
            {

                using (var context = new BookingContext())
                {
                    var result = context.BookingInfos.FirstOrDefault(b => b.BookingId == id);
                    if (result == null)
                        throw new NotFoundException($"Booking with ID {id} was not found.");
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Failed to retrieve booking by id " , ex);
            }
        }

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
               string? notes = null)
        {
            try
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
            catch (Exception ex)
            {
                throw new DAOException("Failed to get Bookings ", ex) ;
            }

        }

        public void AddBooking(BookingInfo booking)
        {
            try
            {

                using (var _context = new BookingContext())
                {
                    _context.BookingInfos.Add(booking);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex) {
                throw new DAOException("Failed to add booking ", ex);
            }
        }

        public void RemoveBooking(int bookingId)
        {
            try
            {

            using (var _context = new BookingContext())
            {
                var booking = _context.BookingInfos.FirstOrDefault(b => b.BookingId == bookingId);
                    if (booking == null)
                        throw new NotFoundException($"Booking with ID {bookingId} was not found.");

                    _context.BookingInfos.Remove(booking);
                    _context.SaveChanges();
                
            }
            }catch(Exception ex)
            {
                throw new DAOException("Failed to Remove Booking", ex);
            }
        }
        public async Task  UpdateBooking(BookingInfo booking)
        {
            try
            {

            using (var _context = new BookingContext())
            {
                var existingBooking = await _context.BookingInfos.FirstOrDefaultAsync(b => b.BookingId == booking.BookingId);

                if (existingBooking == null)
                    throw new NotFoundException($"Booking with Id {booking.BookingId} not found.");

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
            catch (Exception ex)
            {
                throw new DAOException("Failed To Update Booking " , ex);
            }
        }
    }
}