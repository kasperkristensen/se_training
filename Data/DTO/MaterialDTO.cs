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
        public string UserId { get; init; }

        [Required]
        public string AuthorName { get; init; }

        public string VideoUrl { get; init; }

        public IEnumerable<string> TagValues { get; init; }
    }

    public record MaterialDTO : MaterialCreateDTO
    {
        public int Id { get; init; }
    }

    public record MaterialSearchDTO
    {
        public string SearchString { get; init; }
        public int MinimumLikes {get; init;}
        public IEnumerable<Tag> TagValues { get; init; }
    }
}