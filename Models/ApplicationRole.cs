using Microsoft.AspNetCore.Identity;

namespace ASM_1670_Final.Models
{
    public class ApplicationRole: IdentityRole
    {

        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
