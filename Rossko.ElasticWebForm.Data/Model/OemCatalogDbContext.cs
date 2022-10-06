using Microsoft.EntityFrameworkCore;

namespace Rossko.ElasticWebForm.Data.Model
{
    public interface IOemCatalogDbContext
    {
        DbSet<OemCatalog> OemCatalogs { get; set; }
    }
    public partial class OemCatalogDbContext : DbContext, IOemCatalogDbContext
    {
        public OemCatalogDbContext(DbContextOptions<OemCatalogDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<OemCatalog> OemCatalogs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OemCatalog>(entity =>
            {
                entity.ToTable("oem_catalog");
                entity.HasKey(e => e.HitId).HasName("HitId");
                entity.Property(e => e.Event).HasColumnName("Event");
                entity.Property(e => e.IsInternalUser).HasColumnName("IsInternalUser");
                entity.Property(e => e.IsMobile).HasColumnName("IsMobile");
                entity.Property(e => e.MemberId).HasColumnName("MemberId");
                entity.Property(e => e.Timestamp).HasColumnName("Timestamp");
                entity.Property(e => e.VinNumber).HasColumnName("VinNumber");
                entity.Property(e => e.DateIndex).HasColumnName("DateIndex");
                entity.Property(e => e.From).HasColumnName("From");
                entity.Property(e => e.Catalog).HasColumnName("Catalog");
                entity.Property(e => e.CarName).HasColumnName("CarName");

                entity.HasKey(c => c.HitId);
                entity.HasIndex(o => new { o.DateIndex }).IsUnique(false);

            });
            modelBuilder.Entity<OemCatalog>().Property(c => c.HitId).UseCollation("SQL_Latin1_General_CP1_CS_AS");
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
