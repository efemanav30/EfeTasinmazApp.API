 namespace EfeTasinmazApp.API.Entities.Concrete
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Ilce
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("IlId")]
        public int IlId { get; set; }
        public Il Il { get; set; }

      
    }
}
