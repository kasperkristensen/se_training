using System.ComponentModel.DataAnnotations;

namespace se_training.Data
{
    public record MaterialCreateDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Note { get; set; }
        [Required]
        public string UserId { get; set; }
    }

    public record VideoMaterialCreateDTO : MaterialCreateDTO
    {
        [Required]
        [Url]
        public string VideoUrl { get; set; }
    }

    public record MaterialDTO : MaterialCreateDTO
    {
        public int Id { get; set; }
    }

    public record VideoMaterialDTO : VideoMaterialCreateDTO
    {
        public int Id { get; set; }
    }

        public record MaterialUpdateDTO
    {

        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Note { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}