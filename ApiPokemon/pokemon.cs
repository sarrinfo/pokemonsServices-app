//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ApiPokemon
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;


    public partial class pokemon
    {

        public int Id { get; set; }

        public Nullable<int> hp { get; set; }

        public Nullable<int> cp { get; set; }

        public string name { get; set; }

        public string picture { get; set; }

        public Nullable<System.DateTime> created { get; set; }

        public string type { get; set; }
        


        public string ToJson()
        {
           
            return "{ 'Id' :this.Id, 'hp':this.hp, 'cp':this.cp, 'name' :'this.name', 'picture':'this.picture', 'created':'this.created', 'created':'this.type' }";
        }
    }
}
