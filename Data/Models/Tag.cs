using System;
using System.Collections.Generic;

namespace se_training.Data{
    public class Tag : IBaseModel{
        public int Id { get; set; }

        public string Value { get; set; } 

        public ICollection<Material> Materials { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}