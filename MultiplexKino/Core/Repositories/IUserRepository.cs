using MultiplexKino.Areas.Identity.Data;

namespace MultiplexKino.Core.Repositories
{
    public interface IUserRepository
    {
        ICollection<MultiplexKinoUser> GetUsers();

        MultiplexKinoUser GetUser(string id);

        MultiplexKinoUser UpdateUser(MultiplexKinoUser user);
    }
}
