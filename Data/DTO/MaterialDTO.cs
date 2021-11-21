using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace se_training.Data
{
    public record MaterialCreateDTO
    {
        [Required]
        public string Title { get; init; }
        [Required]
        public string Note { get; init; }

        [Required]
        public string AuthorName { get; init; }

        public string VideoUrl { get; init; }

        public IEnumerable<string> TagValues { get; init; }
    }

    public record MaterialDTO : MaterialCreateDTO
    {
        public int Id { get; init; }
    }
}