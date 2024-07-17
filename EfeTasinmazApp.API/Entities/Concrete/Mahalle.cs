 namespace EfeTasinmazApp.API.Entities.Concrete
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Mahalle
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("IlceId")]
        public int IlceId { get; set; }
        public Ilce Ilce { get; set; }
    }
}
