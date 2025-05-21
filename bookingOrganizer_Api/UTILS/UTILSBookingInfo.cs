using bookingOrganizer_Api.DTO;
using bookingOrganizer_Api.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Reflection;

namespace bookingOrganizer_Api.UTILS
{
    public static class UTILSBookingInfo
    {
        public static DTOBookingInfo ConvertBookingToDTOBooking(BookingInfo booking)
        {
            DTOBookingInfo dtoBooking = new DTOBookingInfo();
            Type bookingType = typeof(BookingInfo);
            Type dtoBookingType = typeof(DTOBookingInfo);

            PropertyInfo[] bookingProperties = bookingType.GetProperties();
            foreach (var bookingProperty in bookingProperties)
            {
                PropertyInfo dtoProperty = dtoBookingType.GetProperty(bookingProperty.Name);
                if (dtoProperty != null && bookingProperty.PropertyType == dtoProperty.PropertyType)
                {
                    object value = bookingProperty.GetValue(booking);
                    dtoProperty.SetValue(dtoBooking, value);
                }
            }

            return dtoBooking;
        }

        public static BookingInfo ConvertDTOBookingToBooking(DTOBookingInfo dtoBooking)
        {
            BookingInfo booking = new BookingInfo();
            Type dtoBookingType = typeof(DTOBookingInfo);
            Type bookingType = typeof(BookingInfo);

            PropertyInfo[] dtoProperties = dtoBookingType.GetProperties();
            foreach (var dtoProperty in dtoProperties)
            {
                PropertyInfo bookingProperty = bookingType.GetProperty(dtoProperty.Name);
                if (bookingProperty != null && dtoProperty.PropertyType == bookingProperty.PropertyType)
                {
                    object value = dtoProperty.GetValue(dtoBooking);
                    bookingProperty.SetValue(booking, value);
                }
            }

            return booking;
        }
        public static ICollection<BookingInfo> ConvertDTOsBookingToBookings(ICollection<DTOBookingInfo> dtoBookings)
        {
            ICollection<BookingInfo> bookingInfos = new List<BookingInfo>();
            foreach (var dtoBookingInfo in dtoBookings)
            {
                bookingInfos.Add(ConvertDTOBookingToBooking(dtoBookingInfo));
            }
            return bookingInfos;
        }

        public static ICollection<DTOBookingInfo> ConvertBookingsToDTOBookings(ICollection<BookingInfo> bookings)
        {
            ICollection<DTOBookingInfo> dtobookingInfos = new List<DTOBookingInfo>();
            foreach (var BookingInfo in bookings)
            {
                dtobookingInfos.Add(ConvertBookingToDTOBooking(BookingInfo));
            }
            return dtobookingInfos;
        }
    }
}
