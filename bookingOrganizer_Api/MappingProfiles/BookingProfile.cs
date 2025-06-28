using AutoMapper;
using bookingOrganizer_Api.DTO;
using bookingOrganizer_Api.Models;
namespace bookingOrganizer_Api.MappingProfiles
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<BookingInfo, DTOBookingInfo>();
            CreateMap<DTOBookingInfo, BookingInfo>();

        }

    }
}
