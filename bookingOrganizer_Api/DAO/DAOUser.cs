using bookingOrganizer_Api.Models;

namespace bookingOrganizer_Api.DAO
{
    public class DAOUser
    {
        private readonly BookingContext _context;
        public DAOUser(BookingContext context)
        {
            _context = context;
        }
        public User getUserByName(string userNameInput)
        {
            return _context.Users.Where(u => u.UserName == userNameInput).FirstOrDefault();

        }

        public void registerUser(User user)
        {

        }
    }
}
