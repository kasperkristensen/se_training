using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace se_training.Data{
    public class Material: IBaseModel {
        public int Id { get; set; }

        [StringLength(50)]
        public string Note { get; set; }

        public string Title { get; set; }

        [StringLength(50)]
        public string AuthorName { get; set; }

        public ICollection<Comment> Comments { get; set; } = null;
        
        public ICollection<Like> Likes { get; set; } = null;

        public ICollection<Tag> Tags { get; set; } = null;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}