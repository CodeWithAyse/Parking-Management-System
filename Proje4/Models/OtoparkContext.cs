using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Proje4.Models
{
    public partial class OtoparkContext : DbContext
    {
        public OtoparkContext()
        {
        }

        public OtoparkContext(DbContextOptions<OtoparkContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AracBilgileri> AracBilgileris { get; set; } = null!;
        public virtual DbSet<FiyatBilgisi> FiyatBilgisis { get; set; } = null!;
        public virtual DbSet<Kat> Kats { get; set; } = null!;
        public virtual DbSet<KullaniciBilgisi> KullaniciBilgisi { get; set; } = null!;
        public virtual DbSet<MusteriBilgileri> MusteriBilgileris { get; set; } = null!;
        public virtual DbSet<Park> Parks { get; set; } = null!;
        public virtual DbSet<RezervasyonBilgisi> RezervasyonBilgisis { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer(" Data Source=LAPTOP-K9HAPJ6P;Initial Catalog=Otopark;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AracBilgileri>(entity =>
            {
                entity.Property(e => e.AracModel).IsFixedLength();

                entity.HasOne(d => d.Musteri)
                    .WithMany(p => p.AracBilgileris)
                    .HasForeignKey(d => d.MusteriId)
                    .HasConstraintName("FK_AracBilgileri_MusteriBilgileri");
            });

            modelBuilder.Entity<FiyatBilgisi>(entity =>
            {
                entity.HasOne(d => d.Musteri)
                    .WithMany(p => p.FiyatBilgisis)
                    .HasForeignKey(d => d.MusteriId)
                    .HasConstraintName("FK_FiyatBilgisi_MusteriBilgileri");
            });

            modelBuilder.Entity<Kat>(entity =>
            {
                entity.Property(e => e.KatId).ValueGeneratedNever();

                entity.Property(e => e.KatNo).IsFixedLength();
            });

            modelBuilder.Entity<MusteriBilgileri>(entity =>
            {
                entity.Property(e => e.MusteriId).ValueGeneratedNever();

                entity.HasOne(d => d.Park)
                    .WithMany(p => p.MusteriBilgileris)
                    .HasForeignKey(d => d.ParkId)
                    .HasConstraintName("FK_MusteriBilgileri_Park");
            });

            modelBuilder.Entity<Park>(entity =>
            {
                entity.Property(e => e.ParkAd).IsFixedLength();

                entity.HasOne(d => d.Kat)
                    .WithMany(p => p.Parks)
                    .HasForeignKey(d => d.KatId)
                    .HasConstraintName("FK_Park_Kat");
            });

            modelBuilder.Entity<RezervasyonBilgisi>(entity =>
            {
                entity.Property(e => e.RezervasyonId).ValueGeneratedNever();

                entity.HasOne(d => d.Arac)
                    .WithMany(p => p.RezervasyonBilgisis)
                    .HasForeignKey(d => d.AracId)
                    .HasConstraintName("FK_RezervasyonBilgisi_AracBilgileri");

                entity.HasOne(d => d.Park)
                    .WithMany(p => p.RezervasyonBilgisis)
                    .HasForeignKey(d => d.ParkId)
                    .HasConstraintName("FK_RezervasyonBilgisi_Park");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
