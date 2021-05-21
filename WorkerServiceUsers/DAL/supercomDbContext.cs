using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WorkerServiceUsers.DAL
{
    public partial class supercomDbContext : DbContext
    {
        public supercomDbContext()
        {
        }

        public supercomDbContext(DbContextOptions<supercomDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TimeZone> TimeZones { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("data source=localhost\\SQLEXPRESS;initial catalog=supercom;integrated security=True;MultipleActiveResultSets=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TimeZone>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PK__TimeZone__737584F733508466");

                entity.Property(e => e.Name)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.CountryCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("Country_code");

                entity.Property(e => e.LatLon)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Notes)
                    .HasMaxLength(176)
                    .IsUnicode(false);

                entity.Property(e => e.PortionOfCountryCovered)
                    .HasMaxLength(73)
                    .IsUnicode(false)
                    .HasColumnName("Portion_of_country_covered");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UtcDstOffset)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("UTC_DST_offset");

                entity.Property(e => e.UtcOffset)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("UTC_offset");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.About).HasMaxLength(1000);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.NextTask).HasColumnType("datetime");

                entity.Property(e => e.LastUpdate).HasColumnType("datetime");

                entity.Property(e => e.PhoneSkype)
                    .HasMaxLength(200)
                    .HasColumnName("PhoneSKype");

                entity.Property(e => e.TimeZone).HasMaxLength(200);

                entity.Property(e => e.WebSite).HasMaxLength(200);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
