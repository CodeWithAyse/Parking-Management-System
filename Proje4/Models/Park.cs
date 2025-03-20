using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proje4.Models
{
    [Table("Park")]
    public partial class Park
    {
        public Park()
        {
            MusteriBilgileris = new HashSet<MusteriBilgileri>();
            RezervasyonBilgisis = new HashSet<RezervasyonBilgisi>();
        }

        [Key]
        [Column("ParkID")]
        public int ParkId { get; set; }
        [StringLength(10)]
        public string? ParkAd { get; set; }
        [Column("KatID")]
        public int? KatId { get; set; }

        [ForeignKey("KatId")]
        [InverseProperty("Parks")]
        public virtual Kat? Kat { get; set; }
        [InverseProperty("Park")]
        public virtual ICollection<MusteriBilgileri> MusteriBilgileris { get; set; }
        [InverseProperty("Park")]
        public virtual ICollection<RezervasyonBilgisi> RezervasyonBilgisis { get; set; }
    }
}
