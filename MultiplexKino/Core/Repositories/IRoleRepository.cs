using Microsoft.AspNetCore.Identity;

namespace MultiplexKino.Core.Repositories
{
    public interface IRoleRepository
    {
        ICollection<IdentityRole> GetRoles();
    }
}
