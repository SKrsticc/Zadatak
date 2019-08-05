using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebZadatak.Models
{
    public partial class ZadatakContext : DbContext
    {
        public ZadatakContext()
        {
        }

        public ZadatakContext(DbContextOptions<ZadatakContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DbelementC> DbelementC { get; set; }
        public virtual DbSet<DbelementP> DbelementP { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-P385AFG;Database=Zadatak;User Id=sa;Password=mssql;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbelementC>(entity =>
            {
                entity.ToTable("DBElementC");

                entity.Property(e => e.Grupa)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.IdElementP)
                    .IsRequired()
                    .HasMaxLength(38);

                entity.HasOne(d => d.IdElementPNavigation)
                    .WithMany(p => p.DbelementC)
                    .HasForeignKey(d => d.IdElementP)
                    .HasConstraintName("FK_ElementC_ElementP");
            });

            modelBuilder.Entity<DbelementP>(entity =>
            {
                entity.HasKey(e => e.IdentifikacioniKod);

                entity.ToTable("DBElementP");

                entity.Property(e => e.IdentifikacioniKod)
                    .HasMaxLength(38)
                    .ValueGeneratedNever();

                entity.Property(e => e.DateAndTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.P).HasColumnName("p");
            });
        }
    }
}
