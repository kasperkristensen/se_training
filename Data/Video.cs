using System.ComponentModel.DataAnnotations;

namespace se_training.Data{
    public class Video : Material {

        [StringLength(50)]
        [Url]
        public string VideoUrl {get; set;}

    }
}