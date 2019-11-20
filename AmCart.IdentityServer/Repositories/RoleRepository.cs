using AmCart.IdentityServer.Contexts;
using AmCart.IdentityServer.Model;
using AmCart.IdentityServer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace AmCart.IdentityServer.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ISDatabaseContext dbContext;

        public RoleRepository(ISDatabaseContext context)
        {
            dbContext = context;
        }

        public async Task<Role> GetRole(string id)
        {
            try
            {
                return await dbContext.Roles.FirstOrDefaultAsync(role => role.Id.Equals(id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Role> GetRoleByName(string name)
        {
            try
            {
                return await dbContext.Roles.FirstOrDefaultAsync(role => role.Name.Equals(name));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
