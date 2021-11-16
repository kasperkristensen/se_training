using System.ComponentModel.DataAnnotations;

namespace se_training.Data
{
    public record LikeCreateDTO
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public int MaterialId { get; set; }
    }

    public record LikeDTO : LikeCreateDTO
    {
        public int Id { get; set; }
    }
}