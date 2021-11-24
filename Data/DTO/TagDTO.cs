using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace se_training.Data
{
    public record TagCreateDTO
    {
        [Required]
        [StringLength(50)]
        public string Value { get; init; }

        [Required]    
        public int MaterialId { get; init; }
    }

    public record TagUpdateDTO
    {
        public int Id { get; init; }

        [Required]
        [StringLength(50)]
        public string Value { get; init; }

        [Required]
        public ICollection<int> MaterialIds { get; init; }
    }
}