using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASM_1670_Final.Models
{
    public class JobApplication
    {
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? Introduction { get; set; }
        public string? CVUrl { get; set; }
        [NotMapped]
        public IFormFile? CurriculumVitae { get; set; }
        public bool? Status { get; set; }
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser? User_Id { get; set; }
    }
}
