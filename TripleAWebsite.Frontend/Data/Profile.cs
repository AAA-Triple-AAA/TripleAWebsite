using System.ComponentModel.DataAnnotations;

namespace TripleAWebsite.Frontend.Data
{
    public class Profile
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string JobTitle { get; set; } = string.Empty;

        [Required]
        [StringLength(2000)]
        public string Bio { get; set; } = string.Empty;

        public string? ProfilePhotoUrl { get; set; }

        public string? ResumeUrl { get; set; }
    }
}