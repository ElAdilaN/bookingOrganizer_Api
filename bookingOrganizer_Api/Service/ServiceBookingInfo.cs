using bookingOrganizer_Api.DTO;
using bookingOrganizer_Api.IDAO;
using bookingOrganizer_Api.Models;
using bookingOrganizer_Api.UTILS;

namespace bookingOrganizer_Api.Service
{
    public class ServiceBookingInfo
    {

        IDAOBookingInfo _daoBookingInfo;
        public ServiceBookingInfo(IDAOBookingInfo daoBookingInfo)
        {
            _daoBookingInfo = daoBookingInfo;
        }


        public DTOBookingInfo getBookingInfoById(int id)
        {
            return UTILSBookingInfo.ConvertBookingToDTOBooking(_daoBookingInfo.GetBookingInfoById(id));
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
          string? notes = null) {

            return UTILSBookingInfo.ConvertBookingsToDTOBookings(_daoBookingInfo.GetBookings(bookingId, groupId, typeBookingId, date, startDate, endDate, address, purchaseMethod, seatNumber, notes));
        }
        public void AddBooking(BookingInfo booking) { 
             _daoBookingInfo.AddBooking(booking);     
        }
        //public void RemoveBooking(int bookingId);
        //public void UpdateBooking(BookingInfo booking);


    }
}
