using System;
using System.ComponentModel.DataAnnotations;

namespace se_training.Data{
    public class Comment : IBaseModel
    {

        public int Id {get; init;}

        [StringLength(50)]
        public string UserId{get; set;}

        [StringLength(50)]
        public string UserName {get; set;}

        public Comment Parent{get; init;} 

        public Material Material{get; init;}

        [StringLength(500)]
        public string Text{get; set;}

        public DateTime CreatedAt { get; set;}

        public DateTime UpdatedAt { get; set;}

        public DateTime? DeletedAt { get; set; }
    }
}