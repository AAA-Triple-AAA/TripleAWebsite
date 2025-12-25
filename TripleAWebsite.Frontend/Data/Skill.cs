using System.ComponentModel.DataAnnotations;

namespace TripleAWebsite.Frontend.Data
{
    public class Skill
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        public string? IconUrl { get; set; }

        public int DisplayOrder { get; set; }
    }
}
