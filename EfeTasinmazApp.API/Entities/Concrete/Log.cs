﻿using System.ComponentModel.DataAnnotations;
using System;

namespace Tasinmaz_Proje.Entities
{
    public class Log
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int KullaniciId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Durum { get; set; }

        [Required]
        [MaxLength(50)]
        public string IslemTip { get; set; }

        [Required]
        [MaxLength(500)]
        public string Aciklama { get; set; }

        [Required]
        public DateTime TarihveSaat { get; set; }

        [Required]
        [MaxLength(50)]
        public string KullaniciTip { get; set; }
    }
}