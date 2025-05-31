using bookingOrganizer_Api.DTO;
using bookingOrganizer_Api.Models;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text.RegularExpressions;

namespace bookingOrganizer_Api.UTILS
{
    public static  class UTILSGroup
    {
        public static DTOGroup ConvertGroupToDTOGroup( Models.Group group)
        {
            DTOGroup dtoGroup = new DTOGroup();
            Type groupType = typeof(Models.Group);
            Type dtoGroupType = typeof(DTOGroup);

            PropertyInfo[] groupProperties = groupType.GetProperties();
            foreach (var groupProperty in groupProperties)
            {
                PropertyInfo dtoProperty = dtoGroupType.GetProperty(groupProperty.Name);
                if (dtoProperty != null && groupProperty.PropertyType == dtoProperty.PropertyType)
                {
                    object value = groupProperty.GetValue(group);
                    dtoProperty.SetValue(dtoGroup, value);
                }
            }

            dtoGroup.BookingInfos = group.BookingInfos != null
                ? UTILSBookingInfo.ConvertBookingsToDTOBookings(group.BookingInfos)
                : new List<DTOBookingInfo>();

            //dtoGroup.GroupMembers = group.GroupMembers != null
            //    ? UTILSGroupMember.ConvertGroupMembersToDTOGroupMembers(group.GroupMembers)
            //    : new List<DTOGroupMember>();

            return dtoGroup; 

        }

        public static Models.Group ConvertDTOGroupToGroup(DTOGroup dtoGroup)
        {
            Models.Group group = new Models.Group();
            Type dtoGroupType = typeof(DTOGroup);
            Type groupType = typeof(Models.Group);

            PropertyInfo[] dtoProperties = dtoGroupType.GetProperties();
            foreach (var dtoProperty in dtoProperties)
            {
                PropertyInfo groupProperty = groupType.GetProperty(dtoProperty.Name);
                if (groupProperty != null && dtoProperty.PropertyType == groupProperty.PropertyType)
                {
                    object value = dtoProperty.GetValue(dtoGroup);
                    groupProperty.SetValue(group, value);
                }
            }

            // Manually convert related collections
            group.BookingInfos = dtoGroup.BookingInfos != null
                ? UTILSBookingInfo.ConvertDTOsBookingToBookings(dtoGroup.BookingInfos)
                : new List<BookingInfo>();

            //group.GroupMembers = dtoGroup.GroupMembers != null
            //    ? UTILSGroupMember.ConvertDTOGroupMembersToGroupMembers(dtoGroup.GroupMembers)
            //    : new List<GroupMember>();

            return group;
        }

        public static ICollection<DTOGroup> ConvertGroupsToDTOGroups(ICollection<Models.Group> groups)
        {
            ICollection<DTOGroup> dtoGroups = new List<DTOGroup>();
            foreach (var group in groups)
            {
                DTOGroup dto = ConvertGroupToDTOGroup(group);
                dtoGroups.Add(dto);
            }
            return dtoGroups;
        }

        public static ICollection<Models.Group> ConvertDTOGroupsToGroups(ICollection<DTOGroup> dtoGroups)
        {
            ICollection<Models.Group> groups = new List<Models.Group>();
            foreach (var dtoGroup in dtoGroups)
            {
                Models.Group group = ConvertDTOGroupToGroup(dtoGroup);
                groups.Add(group);
            }
            return groups;
        }


    }
}
