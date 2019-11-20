using AmCart.IdentityServer.Model;
using System.Threading.Tasks;

namespace AmCart.IdentityServer.Repositories.Interfaces
{
    public interface IPermissionRepository
    {
        Task<Permission> GetPermission(string id);

        Task<Permission> GetPermissionByName(string name);
    }
}
