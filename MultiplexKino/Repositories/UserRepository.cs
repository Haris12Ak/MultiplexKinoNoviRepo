using MultiplexKino.Areas.Identity.Data;
using MultiplexKino.Core.Repositories;

namespace MultiplexKino.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MultiplexKinoDbContext _context;

        public UserRepository(MultiplexKinoDbContext context)
        {
            _context = context;
        }

        public ICollection<MultiplexKinoUser> GetUsers()
        {
            return _context.Users.ToList();
        }

        public MultiplexKinoUser GetUser(string id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public MultiplexKinoUser UpdateUser(MultiplexKinoUser user)
        {
            _context.Update(user);
            _context.SaveChanges();

            return user;
        }
    }
}
