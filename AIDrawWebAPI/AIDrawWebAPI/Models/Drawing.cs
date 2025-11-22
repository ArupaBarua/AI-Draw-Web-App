using System.ComponentModel.DataAnnotations;

namespace AIDrawWebAPI.Models
{
    public class Drawing
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string StrokeData { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
