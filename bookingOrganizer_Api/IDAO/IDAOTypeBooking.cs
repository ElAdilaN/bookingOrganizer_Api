using bookingOrganizer_Api.Models;

namespace bookingOrganizer_Api.IDAO
{
    public interface  IDAOTypeBooking
    {
        public ICollection<TypeBooking> getAllTypeBookings();

        public TypeBooking getTypeBookingById(int id);

        public void addTypeBooking(TypeBooking typeBooking);

        public void RemoveTypeBooking(int id);

        public  Task UpdateBooking(TypeBooking bookingType);

    }
}
