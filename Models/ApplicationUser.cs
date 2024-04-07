using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASM_1670_Final.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Description { get; set; }
        public string? AvartarUrl { get; set; }
        [NotMapped]
        public IFormFile? Avatar { get; set; }
        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }
        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
        public virtual ICollection<IdentityUserToken<string>> Tokens { get; set; }
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
        public virtual Job Job { get; set; }
        public virtual JobApplication JobApplication { get; set; }
    }
}
