using bookingOrganizer_Api.DTO;
using bookingOrganizer_Api.Models;
using System.Reflection;

namespace bookingOrganizer_Api.UTILS
{
    public class UTILSTypeBooking
    {
        public static DTOTypeBooking ConvertTypeBookingToDTOTypeBooking(TypeBooking typeBooking)
        {
            DTOTypeBooking dtoTypeBooking = new DTOTypeBooking();
            Type typeBookingType = typeof(TypeBooking);
            Type dtoTypeBookingType = typeof(DTOTypeBooking);

            PropertyInfo[] typeBookingProperties = typeBookingType.GetProperties();
            foreach (var typeBookingProperty in typeBookingProperties)
            {
                PropertyInfo dtoProperty = dtoTypeBookingType.GetProperty(typeBookingProperty.Name);
                if (dtoProperty != null && typeBookingProperty.PropertyType == dtoProperty.PropertyType)
                {
                    object value = typeBookingProperty.GetValue(typeBooking);
                    dtoProperty.SetValue(dtoTypeBooking, value);
                }
            }

            return dtoTypeBooking;
        }

        public static TypeBooking ConvertDTOTypeBookingToTypeBooking(DTOTypeBooking dtoTypeBooking)
        {
            TypeBooking typeBooking = new TypeBooking();
            Type dtoTypeBookingType = typeof(DTOTypeBooking);
            Type typeBookingType = typeof(TypeBooking);

            PropertyInfo[] dtoProperties = dtoTypeBookingType.GetProperties();
            foreach (var dtoProperty in dtoProperties)
            {
                PropertyInfo typeBookingProperty = typeBookingType.GetProperty(dtoProperty.Name);
                if (typeBookingProperty != null && dtoProperty.PropertyType == typeBookingProperty.PropertyType)
                {
                    object value = dtoProperty.GetValue(dtoTypeBooking);
                    typeBookingProperty.SetValue(typeBooking, value);
                }
            }

            return typeBooking;
        }


        public static ICollection<TypeBooking> ConvertDTOsTypeBookingToTypeBookings(ICollection<DTOTypeBooking> dtoTypeBookings)
        {
            ICollection<TypeBooking> typeBookings = new List<TypeBooking>();
            foreach (var dtoTypeBooking in dtoTypeBookings)
            {
                typeBookings.Add(ConvertDTOTypeBookingToTypeBooking(dtoTypeBooking));
            }
            return typeBookings;
        }

        public static ICollection<DTOTypeBooking> ConvertTypeBookingsToDTOTypeBookings(ICollection<TypeBooking> typeBookings)
        {
            ICollection<DTOTypeBooking> dtoTypeBookings = new List<DTOTypeBooking>();
            foreach (var typeBooking in typeBookings)
            {
                dtoTypeBookings.Add(ConvertTypeBookingToDTOTypeBooking(typeBooking));
            }
            return dtoTypeBookings;
        }

    }
}
