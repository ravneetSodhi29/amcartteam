using AmCart.IdentityServer.Model;
using System.Threading.Tasks;

namespace AmCart.IdentityServer.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<Role> GetRole(string id);

        Task<Role> GetRoleByName(string name);
    }
}
