using Microsoft.AspNetCore.Identity;
using MultiplexKino.Areas.Identity.Data;
using MultiplexKino.Core.Repositories;

namespace MultiplexKino.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly MultiplexKinoDbContext _context;

        public RoleRepository(MultiplexKinoDbContext context)
        {
            _context = context;
        }

        public ICollection<IdentityRole> GetRoles()
        {
            return _context.Roles.ToList();
        }
    }
}
