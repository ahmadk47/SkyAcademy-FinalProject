using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiddingManagementSystem.Application.Interfaces;
using BiddingManagementSystem.Domain.Common;
using BiddingManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace BiddingManagementSystem.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    private readonly ICurrentUserService _currentUserService;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        ICurrentUserService currentUserService)
        : base(options)
    {
        _currentUserService = currentUserService;
    }

    public DbSet<Tender> Tenders { get; set; }
    public DbSet<Bid> Bids { get; set; }
    public DbSet<BidItem> BidItems { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<TenderCategory> TenderCategories { get; set; }
    public DbSet<EvaluationCriteria> EvaluationCriteria { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=AHMAD\\SQLEXPRESS;Database=BiddingManagementDb;Trusted_Connection=True;TrustServerCertificate=True;");
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.CreatedBy = _currentUserService.UserId ?? "system";
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedAt = DateTime.UtcNow;
                    entry.Entity.LastModifiedBy = _currentUserService.UserId ?? "system";
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure entities
        modelBuilder.Entity<Tender>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).IsRequired();
            entity.Property(e => e.ReferenceNumber).IsRequired().HasMaxLength(50);

            entity.HasOne(e => e.Category)
                  .WithMany(c => c.Tenders)
                  .HasForeignKey(e => e.CategoryId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(e => e.Bids)
                  .WithOne(b => b.Tender)
                  .HasForeignKey(b => b.TenderId);

            entity.HasMany(e => e.Documents)
                  .WithOne(d => d.Tender)
                  .HasForeignKey(d => d.TenderId);

            entity.HasMany(e => e.EvaluationCriteria)
                  .WithOne(ec => ec.Tender)
                  .HasForeignKey(ec => ec.TenderId);
        });

        modelBuilder.Entity<Bid>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.TotalBidAmount).HasColumnType("decimal(18,2)");

            entity.HasOne(e => e.Bidder)
                  .WithMany(s => s.Bids)
                  .HasForeignKey(e => e.BidderId);

            entity.HasMany(e => e.BidItems)
                  .WithOne(bi => bi.Bid)
                  .HasForeignKey(bi => bi.BidId);

            entity.HasMany(e => e.Documents)
                  .WithOne(d => d.Bid)
                  .HasForeignKey(d => d.BidId);
        });

        modelBuilder.Entity<BidItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Description).IsRequired();
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FileName).IsRequired().HasMaxLength(255);
            entity.Property(e => e.FilePath).IsRequired().HasMaxLength(500);
            entity.Property(e => e.ContentType).HasMaxLength(50);
        });

        modelBuilder.Entity<TenderCategory>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
        });

        modelBuilder.Entity<EvaluationCriteria>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).IsRequired();
            entity.Property(e => e.Weight).HasColumnType("decimal(5,2)");
        });
    }
}
