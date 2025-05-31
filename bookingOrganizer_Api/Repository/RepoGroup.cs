using bookingOrganizer_Api.DTO;

namespace bookingOrganizer_Api.Repository
{
    public interface  RepoGroup
    {
        public ICollection<DTOGroup> getAllGroups();
        public DTOGroup getGroupById(int groupId);
        public void addGroup(DTOGroup dtoGroup);
        public void removeGroup(int groupId);
        public  Task UpdateGroup(DTOGroup dtoGroup);
    }
}
