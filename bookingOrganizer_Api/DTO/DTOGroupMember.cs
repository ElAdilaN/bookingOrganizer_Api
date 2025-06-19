using bookingOrganizer_Api.Models;
using System.Text.Json.Serialization;

namespace bookingOrganizer_Api.DTO
{
    public class DTOGroupMember
    {
        public int GroupMemberId { get; set; }

        public int GroupId { get; set; }

        public int UserId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual DTOGroup? Group { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual DTOUser? User { get; set; }

    }
}
