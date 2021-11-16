using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace se_training.Data{
    public abstract class Material {
        public int Id {get; set;}

        public DateTime DateUploaded {get; set;}

        public DateTime DateUpdated {get; set;}

        [StringLength(50)]
        public string Note {get; set;}

        public ICollection<Comment> Comments {get; set;} = null;
        
        public ICollection<string> Likes {get; set;} = null;

        public ICollection<Tag> Tags {get; set;} = null;

        

    }
}