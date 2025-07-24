using System;
using System.Collections.Generic;
using FishingLake.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FishingLake.DAL;

public partial class FishingManagementContext : DbContext
{
    public FishingManagementContext()
    {
    }

    public FishingManagementContext(DbContextOptions<FishingManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<FishSpecies> FishSpecies { get; set; }

    public virtual DbSet<Pond> Pond { get; set; }

    public virtual DbSet<PondFish> PondFishes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

    {
        optionsBuilder.UseSqlServer("Server=(local);uid=sa;pwd=123;database=FishingManagement;TrustServerCertificate=True");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Bookings__3214EC070A6EFDE6");

            
            
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .HasDefaultValue("Cash");
            entity.Property(e => e.PaymentTime).HasColumnType("datetime");
            entity.Property(e => e.Price)
                .HasDefaultValue(0m)
                .HasColumnType("money");
            

            entity.HasOne(d => d.Pond).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.PondId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bookings__PondId__59FA5E80");

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bookings__UserId__5AEE82B9");
        });

        modelBuilder.Entity<FishSpecies>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FishSpec__3214EC075E73698E");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Pond>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ponds__3214EC0723F23EB0");

            entity.Property(e => e.Capacity).HasDefaultValue(20);
            entity.Property(e => e.Location).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);

            entity.HasOne(d => d.Owner).WithMany(p => p.Ponds)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ponds__OwnerId__5070F446");
        });

        modelBuilder.Entity<PondFish>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PondFish__3214EC0798BC0420");

            entity.ToTable("PondFish");

entity.HasOne(d => d.Fish).WithMany(p => p.PondFishes)
                .HasForeignKey(d => d.FishId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PondFish__FishId__5629CD9C");

            entity.HasOne(d => d.Pond).WithMany(p => p.PondFishes)
                .HasForeignKey(d => d.PondId)
                .OnDelete(DeleteBehavior.Cascade) // ĐÂY
                .HasConstraintName("FK__PondFish__PondId__5535A963");
        });


        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC079D57D793");

            entity.HasIndex(e => e.Phone, "UQ__Users__5C7E359E050B7FE8").IsUnique();

            entity.Property(e => e.IsVip)
                .HasDefaultValue(false)
                .HasColumnName("IsVIP");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Role).HasDefaultValue(2);
            entity.Property(e => e.TotalBookings).HasDefaultValue(0);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
