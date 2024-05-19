using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Goke.Web.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public List<ApplicationRole>? Roles { get; set; }

        [NotMapped]
        public bool IsLockedOut { get; set; }
    }

}
