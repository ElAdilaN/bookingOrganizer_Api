using bookingOrganizer_Api.DTO;
using bookingOrganizer_Api.Models;
using System.Reflection;

namespace bookingOrganizer_Api.UTILS
{
    public static class UTILSGroupMember
    {
        public static DTOGroupMember ConvertGroupMemberToDTO(GroupMember groupMember)
        {
            DTOGroupMember dto = new DTOGroupMember();
            Type modelType = typeof(GroupMember);
            Type dtoType = typeof(DTOGroupMember);

            PropertyInfo[] modelProps = modelType.GetProperties();
            foreach (var modelProp in modelProps)
            {
                PropertyInfo dtoProp = dtoType.GetProperty(modelProp.Name);
                if (dtoProp != null && modelProp.PropertyType == dtoProp.PropertyType)
                {
                    object value = modelProp.GetValue(groupMember);
                    dtoProp.SetValue(dto, value);
                }
            }

            // Convert navigation properties (manually)
            if (groupMember.Group != null)
                dto.Group = UTILSGroup.ConvertGroupToDTOGroup(groupMember.Group);

            if (groupMember.User != null)
                dto.User = UTILSUser.convertirUserToDTOUser(groupMember.User);

            return dto;
        }

        public static GroupMember ConvertDTOToGroupMember(DTOGroupMember dto)
        {
            GroupMember model = new GroupMember();
            Type dtoType = typeof(DTOGroupMember);
            Type modelType = typeof(GroupMember);

            PropertyInfo[] dtoProps = dtoType.GetProperties();
            foreach (var dtoProp in dtoProps)
            {
                PropertyInfo modelProp = modelType.GetProperty(dtoProp.Name);
                if (modelProp != null && dtoProp.PropertyType == modelProp.PropertyType)
                {
                    object value = dtoProp.GetValue(dto);
                    modelProp.SetValue(model, value);
                }
            }

            // Convert navigation properties (manually)
            if (dto.Group != null)
                model.Group = UTILSGroup.ConvertDTOGroupToGroup(dto.Group);

            if (dto.User != null)
                model.User = UTILSUser.ConvertDTOUserToUser(dto.User);

            return model;
        }

        public static ICollection<GroupMember> ConvertDTOsToGroupMembers(ICollection<DTOGroupMember> dtos)
        {
            ICollection<GroupMember> members = new List<GroupMember>();
            foreach (var dto in dtos)
            {
                members.Add(ConvertDTOToGroupMember(dto));
            }
            return members;
        }

        public static ICollection<DTOGroupMember> ConvertGroupMembersToDTOs(ICollection<GroupMember> members)
        {
            ICollection<DTOGroupMember> dtos = new List<DTOGroupMember>();
            foreach (var member in members)
            {
                dtos.Add(ConvertGroupMemberToDTO(member));
            }
            return dtos;
        }
    }
}
