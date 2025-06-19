using bookingOrganizer_Api.Models;
using System.Text.Json.Serialization;

namespace bookingOrganizer_Api.DTO
{
    public class DTOGroup
    {
        public int GroupId { get; set; }

        public string GroupName { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual ICollection<DTOBookingInfo>? BookingInfos { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual ICollection<DTOGroupMember>? GroupMembers { get; set; }
    }
}
