using Microsoft.AspNetCore.Identity;

namespace ASM_1670_Final.Models
{
    public class ApplicationUserRole: IdentityUserRole<string>
    {
        public virtual ApplicationUser User { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }
}
