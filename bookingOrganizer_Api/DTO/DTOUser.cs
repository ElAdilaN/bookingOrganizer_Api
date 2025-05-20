using bookingOrganizer_Api.Models;

namespace bookingOrganizer_Api.DTO
{
    public class DTOUser
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public virtual ICollection<DTOGroupMember> GroupMembers { get; set; } = new List<DTOGroupMember>();
    }
}
