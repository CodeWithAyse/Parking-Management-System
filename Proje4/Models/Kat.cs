using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proje4.Models
{
    [Table("Kat")]
    public partial class Kat
    {
        public Kat()
        {
            Parks = new HashSet<Park>();
        }

        [Key]
        [Column("KatID")]
        public int KatId { get; set; }
        [StringLength(10)]
        public string? KatNo { get; set; }

        [InverseProperty("Kat")]
        public virtual ICollection<Park> Parks { get; set; }
    }
}
