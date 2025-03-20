using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proje4.Models
{
    [Table("AracBilgileri")]
    public partial class AracBilgileri
    {
        public AracBilgileri()
        {
            RezervasyonBilgisis = new HashSet<RezervasyonBilgisi>();
        }

        [Key]

        [Column("AracID")]
        public int AracId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        [DisplayName("Araç Plaka")]
        public string? AracPlaka { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        [DisplayName("Araç Marka")]
        public string? AracMarka { get; set; }
        [StringLength(20)]
        [DisplayName("Araç Model")]
        public string? AracModel { get; set; }
        [Column("MusteriID")]
        [DisplayName("Müşteri Ad")]
        public int? MusteriId { get; set; }
        [StringLength(150)]
        [Unicode(false)]
        [DisplayName("Araç Resmi")]
        public string? AracPhoto { get; set; }
        [NotMapped]
        [DisplayName("Araç Resmi")]
        public IFormFile ImageFile { get; set; }

        [ForeignKey("MusteriId")]
        [InverseProperty("AracBilgileris")]
        public virtual MusteriBilgileri? Musteri { get; set; }
        [InverseProperty("Arac")]
        public virtual ICollection<RezervasyonBilgisi> RezervasyonBilgisis { get; set; }
    }
}
