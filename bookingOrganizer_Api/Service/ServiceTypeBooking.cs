using bookingOrganizer_Api.DAO;
using bookingOrganizer_Api.DTO;
using bookingOrganizer_Api.Exceptions;
using bookingOrganizer_Api.IDAO;
using bookingOrganizer_Api.Models;
using bookingOrganizer_Api.UTILS;

namespace bookingOrganizer_Api.Service
{
    public class ServiceTypeBooking
    {
        private readonly IDAOTypeBooking _daoTypeBooking;
        public ServiceTypeBooking(IDAOTypeBooking idaoTypeBooking)
        {
            _daoTypeBooking = idaoTypeBooking;
        }

        public ICollection<DTOTypeBooking> getAllTypeBookings()
        {
            try
            {
                return UTILSTypeBooking.ConvertTypeBookingsToDTOTypeBookings(_daoTypeBooking.getAllTypeBookings());
            }
            catch (Exception ex)
            {
                throw new TypeBookingServiceException("Failed to retrieve all booking types. Error: " + ex.Message, ex);
            }

        }

        public DTOTypeBooking getTypeBookingById(int id)
        {
            try
            {
                return UTILSTypeBooking.ConvertTypeBookingToDTOTypeBooking(_daoTypeBooking.getTypeBookingById(id));
            }
            catch (Exception ex)
            {
                throw new TypeBookingServiceException("Failed to retrieve booking type by id . Error: " + ex.Message, ex);
            }
        }

        public void addTypeBooking(DTOTypeBooking dtotypeBooking)
        {
            try
            {

                TypeBooking typeBooking = UTILSTypeBooking.ConvertDTOTypeBookingToTypeBooking(dtotypeBooking);
                _daoTypeBooking.addTypeBooking(typeBooking);
            }
            catch (Exception ex) {
                throw new TypeBookingServiceException("Failed to add booking type : ", ex);
            }
        }

        public void RemoveTypeBooking(int typeBookingId)
        {
            try
            {
                _daoTypeBooking.RemoveTypeBooking(typeBookingId);
            }
            catch (KeyNotFoundException)
            {
                throw new NotFoundException($"Type Booking with ID {typeBookingId} not found.");
            }
            catch (Exception ex)
            {
                throw new BookingServiceException("Error removing booking type .", ex);
            }
        }

        public async Task UpdateBooking(DTOTypeBooking dtoBookingType)
        {
            try
            {
                TypeBooking typeBooking = UTILSTypeBooking.ConvertDTOTypeBookingToTypeBooking(dtoBookingType);
                await _daoTypeBooking.UpdateBooking(typeBooking);
            }
            catch (KeyNotFoundException)
            {
                throw new NotFoundException($"Booking with ID {dtoBookingType.TypeBookingId} not found.");
            }
            catch (Exception ex)
            {
                throw new TypeBookingServiceException("Error updating booking type.", ex);
            }
        }
    }
}
