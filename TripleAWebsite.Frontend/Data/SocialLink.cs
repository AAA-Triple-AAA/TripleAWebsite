using System.ComponentModel.DataAnnotations;

namespace TripleAWebsite.Frontend.Data
{
    public class SocialLink
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^(https?://|mailto:).+", ErrorMessage = "Url must start with http://, https://, or mailto:")]
        public string Url { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string IconClass { get; set; } = string.Empty; // e.g., "bi bi-github"

        public int DisplayOrder { get; set; }
    }
}
