using bookingOrganizer_Api.Models;

namespace bookingOrganizer_Api.DTO
{
    public class DTOGroupMember
    {
        public int GroupMemberId { get; set; }

        public int GroupId { get; set; }

        public int UserId { get; set; }

        public virtual DTOGroup Group { get; set; }

        public virtual DTOUser User { get; set; }
    }
}
