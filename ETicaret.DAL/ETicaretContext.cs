using ETicaret.Entities;
using ETicaret.Entities.Authentications;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ETicaret.DAL
{
   public class ETicaretContext:IdentityDbContext<AppUser,AppRole,int>
    {
        public  ETicaretContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Urun> Urunler { get; set; }
        public  DbSet<Yorum>Yorumlar { get; set; }
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<Sepet> Sepetler { get; set; }
        public DbSet<SepetUrun> SepetUrunler { get; set; }
        public DbSet<Siparis> Siparisler { get; set; }
        public DbSet<SiparisDetay> SiparisDetaylar { get; set; }
        public  DbSet<Marka>Markalar { get; set; }
        public DbSet<Resim> Resimler { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>()
                        .HasOne(a => a.Sepet)
                        .WithOne(s => s.AppUser)
                        .HasForeignKey<Sepet>(fk => fk.AppUserId);
            modelBuilder.Entity<Urun>()
                .HasOne(u => u.Kategori)
                .WithMany(k => k.Urunler)
                .HasForeignKey(f => f.KategoriId);

            modelBuilder.Entity<Yorum>()
                .HasOne(y => y.Urun)
                .WithMany(u => u.Yorumlar)
                .HasForeignKey(f => f.UrunId);

            modelBuilder.Entity<Urun>()
                .HasOne(u => u.Marka)
                .WithMany(m => m.Urunler)
                .HasForeignKey(f => f.MarkaId);

            modelBuilder.Entity<Resim>()
                .HasOne(r => r.Urun)
                .WithMany(u => u.Resimler)
                .HasForeignKey(f => f.UrunId);

            modelBuilder.Entity<SiparisDetay>()
                .HasKey(k => new { k.SiparisId, k.UrunId });

            modelBuilder.Entity<SiparisDetay>()
                .HasOne(sd => sd.Urun)
                .WithMany(u => u.Siparisler)
                .HasForeignKey(f => f.UrunId);

            modelBuilder.Entity<SiparisDetay>()
                .HasOne(sd => sd.Siparis)
                .WithMany(u => u.Detaylar)
                .HasForeignKey(f => f.SiparisId);

            modelBuilder.Entity<SepetUrun>()
                .HasKey(k => new { k.SepetId, k.UrunId });

            modelBuilder.Entity<SepetUrun>()
                .HasOne(su => su.Sepet)
                .WithMany(s => s.Urunler)
                .HasForeignKey(f => f.SepetId);

            modelBuilder.Entity<SepetUrun>()
                .HasOne(su => su.Urun)
                .WithMany(s => s.Sepetler)
                .HasForeignKey(f => f.UrunId);
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = 1,
                UserName = "kubra",
                NormalizedUserName = "KUBRA",
                Email = "kubramogul06@gmail.com",
                NormalizedEmail = "KUBRAMOGUL06@GMAIL.COM",
                PasswordHash = new PasswordHasher<AppUser>().HashPassword(null, "12345"),
                SecurityStamp = ""
            });
            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = 1,
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            }, 
            new AppRole
            {
                Id = 2,
                Name = "Moderator",
                NormalizedName = "MODERATOR"
            },
            new AppRole
            {
                Id = 3,
                Name = "Editor",
                NormalizedName = "EDITOR"
            });
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int>()
            {
                RoleId = 1,
                UserId = 1
            },
            new IdentityUserRole<int>()
            {
                RoleId = 2,
                UserId = 1
            },
            new IdentityUserRole<int>()
            {
                RoleId = 3,
                UserId = 1
            });

            base.OnModelCreating(modelBuilder);
        }
    }
   
}
