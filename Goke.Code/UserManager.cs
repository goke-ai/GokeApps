using System;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Identity
{
    public class UserManager<T>
    {
        public Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, object token)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> FindByNameAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsInRoleAsync(ApplicationUser user, string role)
        {
            throw new NotImplementedException();
        }
    }
}