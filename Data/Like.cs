using System.ComponentModel.DataAnnotations;

namespace se_training.Data
{
    public class Like
    {
        public int Id {get; set;}

        [StringLength(50)]
        public string UserId {get; set;}

        public Material Material {get; set;}
    }
}