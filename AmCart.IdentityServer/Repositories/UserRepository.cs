using AmCart.IdentityServer.Contexts;
using AmCart.IdentityServer.Model;
using AmCart.IdentityServer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AmCart.IdentityServer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ISDatabaseContext dbContext;
        private readonly IRoleRepository roleRepository;
        private readonly IPermissionRepository permissionRepository;

        public UserRepository(ISDatabaseContext context, IRoleRepository roleRepository, IPermissionRepository permissionRepository)
        {
            dbContext = context;
            this.roleRepository = roleRepository;
            this.permissionRepository = permissionRepository;
        }

        public async Task<string> CreateAsync(User user)
        {
            try
            {
                var role = await roleRepository.GetRoleByName("Customer");
                user.Role = role;
                var permission = await permissionRepository.GetPermissionByName("Admin");
                user.Permissions = new List<Permission>() { permission };
                await dbContext.Users.AddAsync(user);
                return user.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> GetUserAsync(string id)
        {
            try
            {
                return await dbContext.Users.FirstOrDefaultAsync(user => user.Id.Equals(id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            try
            {
                return await dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> ValidateUserAsync(string username, string password)
        {
            try
            {
                return await dbContext.Users.FirstOrDefaultAsync(u => u.Username.Equals(username) && u.Password.Equals(password));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
