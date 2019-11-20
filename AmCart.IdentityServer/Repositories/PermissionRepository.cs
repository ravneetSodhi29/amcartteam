using AmCart.IdentityServer.Contexts;
using AmCart.IdentityServer.Model;
using AmCart.IdentityServer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace AmCart.IdentityServer.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly ISDatabaseContext dbContext;

        public PermissionRepository(ISDatabaseContext context)
        {
            dbContext = context;
        }

        public async Task<Permission> GetPermission(string id)
        {
            try
            {
                return await dbContext.Permissions.FirstOrDefaultAsync(permission => permission.Id.Equals(id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Permission> GetPermissionByName(string name)
        {
            try
            {
                return await dbContext.Permissions.FirstOrDefaultAsync(permission => permission.Name.Equals(name));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
