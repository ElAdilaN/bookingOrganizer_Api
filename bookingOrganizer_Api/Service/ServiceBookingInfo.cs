using bookingOrganizer_Api.DTO;
using bookingOrganizer_Api.Exceptions;
using bookingOrganizer_Api.IDAO;
using bookingOrganizer_Api.Models;
using bookingOrganizer_Api.UTILS;

namespace bookingOrganizer_Api.Service
{
    public class ServiceBookingInfo
    {

        private readonly IDAOBookingInfo _daoBookingInfo;
        public ServiceBookingInfo(IDAOBookingInfo daoBookingInfo)
        {
            _daoBookingInfo = daoBookingInfo;
        }


        public DTOBookingInfo getBookingInfoById(int id)
        {
            try
            {
                return UTILSBookingInfo.ConvertBookingToDTOBooking(_daoBookingInfo.GetBookingInfoById(id));
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new BookingServiceException("Error retrieving booking by ID.", ex);
            }
        }

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
          string? notes = null)
        {
            try
            {
                return UTILSBookingInfo.ConvertBookingsToDTOBookings(_daoBookingInfo.GetBookings(bookingId, groupId, typeBookingId, date, startDate, endDate, address, purchaseMethod, seatNumber, notes));
            }
            catch (Exception ex)
            {
                throw new BookingServiceException("Error retrieving bookings.", ex);
            }
        }
        public void AddBooking(BookingInfo booking)
        {
            try
            {
                _daoBookingInfo.AddBooking(booking);
            }
            catch (Exception ex)
            {
                throw new BookingServiceException("Error adding booking.", ex);
            }
        }

        public void RemoveBooking(int bookingId)
        {
            try
            {
                _daoBookingInfo.RemoveBooking(bookingId);
            }
            catch (Exception ex)
            {
                throw new BookingServiceException("Error removing booking.", ex);
            }
        }

        public async Task UpdateBooking(BookingInfo booking)
        {
            try
            {
                await _daoBookingInfo.UpdateBooking(booking);
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new BookingServiceException("Error updating booking.", ex);
            }
        }

    }
}
