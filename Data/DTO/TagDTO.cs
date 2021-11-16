using System.ComponentModel.DataAnnotations;

namespace se_training.Data
{
    public record TagCreateDTO
    {
        [Required]
        [StringLength(50)]
        public string Value { get; set; }
    }

    public record TagDTO : TagCreateDTO
    {
        public int Id { get; set; }
    }
}