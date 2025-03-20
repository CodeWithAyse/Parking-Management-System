using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proje4.Models
{
    [Keyless]
    [Table("KullaniciBilgisi")]
    public partial class KullaniciBilgisi
    {
        [StringLength(50)]
        [Unicode(false)]
        [DisplayName("Kullanıcı Adı")]
        public string KullaniciAd { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        [DisplayName("Kullanıcı Şifre")]
        public string KullaniciSifre { get; set; } = null!;
        public bool LoggedStatus { get; set; }
    }
}
