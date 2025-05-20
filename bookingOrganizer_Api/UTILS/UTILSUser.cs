using bookingOrganizer_Api.DTO;
using bookingOrganizer_Api.Models;
using System.Reflection;

namespace bookingOrganizer_Api.UTILS
{
    public class UTILSUser
    {
        public DTOUser convertirUserToDTOUser(User userObj)
        {
            DTOUser _dtoUser = new DTOUser();

            DTOUser dtoUser = new DTOUser();
            Type userType = typeof(User);
            Type dtoUserType = typeof(DTOUser);

            PropertyInfo[] userProperties = userType.GetProperties();
            foreach (var userProperty in userProperties)
            {
                PropertyInfo dtoProperty = dtoUserType.GetProperty(userProperty.Name);
                if (dtoProperty != null && userProperty.PropertyType == dtoProperty.PropertyType)
                {
                    object value = userProperty.GetValue(userObj);
                    dtoProperty.SetValue(dtoUser, value);
                }
            }
            return dtoUser;
        }

        public User ConvertDTOUserToUser(DTOUser dtoUser)
        {
            User user = new User();
            Type dtoUserType = typeof(DTOUser);
            Type userType = typeof(User);

            PropertyInfo[] dtoProperties = dtoUserType.GetProperties();
            foreach (var dtoProperty in dtoProperties)
            {
                PropertyInfo userProperty = userType.GetProperty(dtoProperty.Name);
                if (userProperty != null && dtoProperty.PropertyType == userProperty.PropertyType)
                {
                    object value = dtoProperty.GetValue(dtoUser);
                    userProperty.SetValue(user, value);
                }
            }

            return user;
        }

    }
}
