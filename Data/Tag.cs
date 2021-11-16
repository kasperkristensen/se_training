using System.Collections.Generic;

namespace se_training.Data{
    public class Tag {
        public int Id {get; set;}

        public string Value {get; set;} 
        //Hvordan pokker laver vi tags på en måde der ikke bliver en total rodebunke? Enums?
        //Eller kan sådan noget blive løst med UI der kun tillader oprettelse af visse ting?

        public ICollection<Material> Materials {get; set;}
    }
}