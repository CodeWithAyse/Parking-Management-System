using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proje4.Models
{
    [Table("RezervasyonBilgisi")]
    public partial class RezervasyonBilgisi
    {
        [Key]
        [Column("RezervasyonID")]
        public int RezervasyonId { get; set; }
        [Column("AracID")]
        [DisplayName("Araç Plaka")]
        public int? AracId { get; set; }
        [Column("ParkID")]
        [DisplayName("Park Yeri Bilgisi")]
        public int? ParkId { get; set; }
        [DisplayName("Giriş Saati")]
        public TimeSpan? StartTime { get; set; }
        [DisplayName("Çıkış Saati")]
        public TimeSpan? FinishTime { get; set; }

        [ForeignKey("AracId")]
        [InverseProperty("RezervasyonBilgisis")]
        public virtual AracBilgileri? Arac { get; set; }
        [ForeignKey("ParkId")]
        [InverseProperty("RezervasyonBilgisis")]
        public virtual Park? Park { get; set; }
    }
}
