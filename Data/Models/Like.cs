using System;
using System.ComponentModel.DataAnnotations;

namespace se_training.Data
{
    public class Like : IBaseModel
    {
        public int Id {get; set;}

        [StringLength(50)]
        public string UserId {get; set;}

        public Material Material {get; set;}
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}