using bookingOrganizer_Api.DTO;

namespace bookingOrganizer_Api.Repository
{
    public interface RepoTypeBooking
    {
        public ICollection<DTOTypeBooking> getAllTypeBookings();


        public DTOTypeBooking getTypeBookingById(int id);

        public void addTypeBooking(DTOTypeBooking dtotypeBooking);

        public void RemoveTypeBooking(int typeBookingId);

        public Task UpdateBooking(DTOTypeBooking dtoBookingType);


    }
}
