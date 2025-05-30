using System.Text.RegularExpressions;
using bookingOrganizer_Api.Models;
namespace bookingOrganizer_Api.IDAO
{
    public interface  IDAOGroup
    {
        public ICollection<Models.Group> getAllGroups();
        public Models.Group getGroupById(int groupId);

        public void addGroup(Models.Group group);

        public void RemoveGroup(int groupId);
        public Task UpdateGroup(Models.Group updatedGroup);

    }
}
