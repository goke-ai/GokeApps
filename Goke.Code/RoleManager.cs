using System;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Identity
{
    public class RoleManager<T>
    {
        public Task<IdentityRole> FindByNameAsync(string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> CreateAsync(IdentityRole role)
        {
            throw new NotImplementedException();
        }
    }
}