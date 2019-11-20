using System.Collections.Generic;

namespace AmCart.IdentityServer.Model
{
    public class User : DomainBase
    {
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailId { get; set; }

        public string Password { get; set; }

        public bool IsVerified { get; set; }

        public Role Role { get; set; }

        public List<Permission> Permissions { get; set; }
    }
}
