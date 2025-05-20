using bookingOrganizer_Api.Models;

namespace bookingOrganizer_Api.DTO
{
    public class DTOGroup
    {
        public int GroupId { get; set; }

        public string GroupName { get; set; }

        public virtual ICollection<DTOBookingInfo> BookingInfos { get; set; } = new List<DTOBookingInfo>();

        public virtual ICollection<DTOGroupMember> GroupMembers { get; set; } = new List<DTOGroupMember>();

    }
}
