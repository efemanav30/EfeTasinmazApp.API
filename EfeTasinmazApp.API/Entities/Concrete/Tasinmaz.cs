/* using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using EfeTasinmazApp.API.Entities.Concrete;
using EfeTasinmazApp.API.DataAccess;
using EfeTasinmazApp.API.Entities.Concrete;


namespace TasinmazYonetimi2.Entities.Concrete
{
    public class Tasinmaz
    {
        public int TasinmazId { get; set; }


        public string Ada { get; set; }
        public string Parsel { get; set; }
        public string Nitelik { get; set; } // Arsa-Tarla-Mesken


        public string KoordinatBilgileri { get; set; }

        [ForeignKey("MahalleId")]

        public int MahalleId { get; set; }

        public virtual Mahalle Mahalle { get; set; }

    }
}
*/
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfeTasinmazApp.API.Entities.Concrete
{
    public class Tasinmaz
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Ada { get; set; }

        [Required]
        public string Parsel { get; set; }

        [Required]
        [StringLength(100)]
        public string Nitelik { get; set; }

        [Required]
        public string KoordinatBilgileri{ get; set; }

        [Required]
        public string Adres { get; set; }

        [Required]
        [ForeignKey("Mahalle")]
        public int MahalleId { get; set; }

        public Mahalle Mahalle { get; set; }
    }
}
