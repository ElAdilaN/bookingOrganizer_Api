using bookingOrganizer_Api.Models;

namespace bookingOrganizer_Api.DAO
{
    public class DAOUser
    {
        public User getUserByName(string userNameInput)
        {
            using (var context = new BookingContext())
            {
                return context.Users.Where(u => u.UserName == userNameInput).FirstOrDefault();
            }
        }

        public void registerUser(User user) { 
            
        }
    }
}
