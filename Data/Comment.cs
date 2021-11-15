using System.ComponentModel.DataAnnotations;

namespace se_training.Data{
    public class Comment {

        public int Id {get; set;}

        [StringLength(50)]
        public string UserId{get; set;}

        public Comment Parent{get; set;} 

        public Material Material{get; set;}

        /* Burde man have en subklasse Reply?
         * Enten det, ellers skal Parent nogle gange være  null,
         * og hvis det er et reply, skal Material så være null?
         * Eller skal Parent kunne være enten Material eller Comment?
         */

        [StringLength(500)] //Hvor lang må en kommentar være?
         public string Text{get; set;}     

        
    }
}