using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proje4.Models
{
    [Table("MusteriBilgileri")]
    public partial class MusteriBilgileri
    {
        public MusteriBilgileri()
        {
            AracBilgileris = new HashSet<AracBilgileri>();
            FiyatBilgisis = new HashSet<FiyatBilgisi>();
        }

        [Key]
        [Column("MusteriID")]
        [DisplayName("Müşteri ID")]
        public int MusteriId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        [DisplayName("Müşteri Ad")]
        public string? MusteriAd { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        [DisplayName("Müşteri Soyad")]
        public string? MusteriSoyad { get; set; }
        [StringLength(11)]
        [Unicode(false)]
        [DisplayName("TC Numarası")]
        public string? MusteriTc { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        [DisplayName("Telefon Numarası")]
        public string? MusteriTel { get; set; }
        [Column("ParkID")]
        [DisplayName("Park Yeri Adı")]
        public int? ParkId { get; set; }

        [ForeignKey("ParkId")]
        [InverseProperty("MusteriBilgileris")]
        public virtual Park? Park { get; set; }
        [InverseProperty("Musteri")]
        public virtual ICollection<AracBilgileri> AracBilgileris { get; set; }
        [InverseProperty("Musteri")]
        public virtual ICollection<FiyatBilgisi> FiyatBilgisis { get; set; }
    }
}
