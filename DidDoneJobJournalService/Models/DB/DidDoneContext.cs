using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DidDoneJobJournalService.Models.DB
{
    public partial class DidDoneContext : DbContext
    {
        public DidDoneContext()
        {
        }

        public DidDoneContext(DbContextOptions<DidDoneContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<CustomerSite> CustomerSite { get; set; }
        public virtual DbSet<StateCodes> StateCodes { get; set; }
        public virtual DbSet<Visits> Visits { get; set; }
        public virtual DbSet<VisitWorker> VisitWorker { get; set; }
        public virtual DbSet<Workers> Workers { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=DAVID1\\SQLEXPRESS;Database=DidDone;Trusted_Connection=True;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EmailAddress)
                    .HasColumnName("EMailAddress")
                    .HasMaxLength(100);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StreetAddress).HasMaxLength(72);

                entity.Property(e => e.Zip).HasMaxLength(10);

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customers_StateCodes");
            });

            modelBuilder.Entity<CustomerSite>(entity =>
            {
                entity.Property(e => e.CustomerSiteId).ValueGeneratedNever();

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EmailAddress)
                    .HasColumnName("EMailAddress")
                    .HasMaxLength(100);

                entity.Property(e => e.Lat).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Long).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.SiteAddress).HasMaxLength(72);

                entity.Property(e => e.SiteName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerSite)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerSite_Customers");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.CustomerSite)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerSite_StateCodes");
            });

            modelBuilder.Entity<StateCodes>(entity =>
            {
                entity.HasKey(e => e.StateId);

                entity.Property(e => e.StateId).ValueGeneratedNever();

                entity.Property(e => e.DomainName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasDefaultValueSql("(N'US')");

                entity.Property(e => e.StateCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.StateName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Visits>(entity =>
            {
                entity.HasKey(e => e.VisitId);

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.HasOne(d => d.CustomerSite)
                    .WithMany(p => p.Visits)
                    .HasForeignKey(d => d.CustomerSiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Visits_CustomerSite");
            });

            modelBuilder.Entity<VisitWorker>(entity =>
            {
                entity.HasKey(e => new { e.VisitId, e.WorkerId });

                entity.ToTable("Visit_Worker");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.Rate).HasColumnType("money");

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.HasOne(d => d.Visit)
                    .WithMany(p => p.VisitWorker)
                    .HasForeignKey(d => d.VisitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Visit_Worker_Visits");

                entity.HasOne(d => d.Worker)
                    .WithMany(p => p.VisitWorker)
                    .HasForeignKey(d => d.WorkerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Visit_Worker_Workers");
            });

            modelBuilder.Entity<Workers>(entity =>
            {
                entity.HasKey(e => e.WorkerId);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Rate).HasColumnType("money");

                entity.Property(e => e.StartDate).HasColumnType("date");
            });
        }
    }
}
