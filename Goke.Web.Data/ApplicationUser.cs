using Goke.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Goke.Web.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [NotMapped]
        public bool IsLockedOut { get; set; }
        [NotMapped]
        public List<ApplicationRole>? Roles { get; set; }

        public Guid? UserDetailId { get; set; }
        public UserDetail? UserDetail { get; set; }
    }

}
