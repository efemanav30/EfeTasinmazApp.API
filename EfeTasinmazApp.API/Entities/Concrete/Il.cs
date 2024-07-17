 namespace EfeTasinmazApp.API.Entities.Concrete
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Il
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }


    }
}
