using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proje4.Models
{
    [Table("FiyatBilgisi")]
    public partial class FiyatBilgisi
    {
        [Key]
        [Column("FiyatID")]
        public int FiyatId { get; set; }
        [Column(TypeName = "smallmoney")]
        [DisplayName("Saat Fiyatı")]
        public decimal? Fiyat { get; set; }
        [DisplayName("Süre(Saat)")]
        public int? Saat { get; set; }
        [Column(TypeName = "smallmoney")]
        [DisplayName("Toplam Ödeme")]
        public decimal? ToplamTutar { get; set; }
        [Column("MusteriID")]

        [DisplayName("Müşteri Ad")]
        public int? MusteriId { get; set; }

        [ForeignKey("MusteriId")]
        [InverseProperty("FiyatBilgisis")]
        public virtual MusteriBilgileri? Musteri { get; set; }
    }
}
