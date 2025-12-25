using System.ComponentModel.DataAnnotations;

namespace TripleAWebsite.Frontend.Data
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        [Url]
        public string? GithubUrl { get; set; }

        [Url]
        public string? LiveUrl { get; set; }

        public int DisplayOrder { get; set; }
    }
}