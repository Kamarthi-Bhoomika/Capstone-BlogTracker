using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Models;

public partial class CapstoneDbContext : DbContext
{
    public CapstoneDbContext()
    {
    }

    public CapstoneDbContext(DbContextOptions<CapstoneDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdminInfo> AdminInfos { get; set; }

    public virtual DbSet<BlogInfo> BlogInfos { get; set; }

    public virtual DbSet<EmpInfo> EmpInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=DESKTOP-CHNJ5UD;database=capstoneDb;trusted_connection=true;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdminInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AdminInf__3214EC0775B2E2BA");

            entity.ToTable("AdminInfo");

            entity.Property(e => e.EmailId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<BlogInfo>(entity =>
        {
            entity.HasKey(e => e.BlogId).HasName("PK__BlogInfo__54379E305CBF641D");

            entity.ToTable("BlogInfo");

            entity.Property(e => e.BlogUrl)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.DateOfCreation).HasColumnType("datetime");
            entity.Property(e => e.EmpEmailId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Subject)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EmpInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EmpInfo__3214EC07E8D89E98");

            entity.ToTable("EmpInfo");

            entity.HasIndex(e => e.EmailId, "UQ__EmpInfo__7ED91ACEA30178E3").IsUnique();

            entity.Property(e => e.DateOfJoining).HasColumnType("datetime");
            entity.Property(e => e.EmailId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
