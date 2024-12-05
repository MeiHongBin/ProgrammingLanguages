using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProgrammingLanguages.Models;

public partial class LanguageProjectContext : DbContext
{
    public LanguageProjectContext()
    {
    }

    public LanguageProjectContext(DbContextOptions<LanguageProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<LanguageFeature> LanguageFeatures { get; set; }

    public virtual DbSet<LanguageVersion> LanguageVersions { get; set; }

    public virtual DbSet<LearningProgress> LearningProgresses { get; set; }

    public virtual DbSet<LearningResource> LearningResources { get; set; }

    public virtual DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }

    public virtual DbSet<ProgrammingLanguageFeature> ProgrammingLanguageFeatures { get; set; }

    public virtual DbSet<User> Users { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=LanguageProject;Integrated Security=True;Encrypt=true;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LanguageFeature>(entity =>
        {
            entity.HasKey(e => e.FeatureId).HasName("PK__Language__82230A290470A051");

            entity.Property(e => e.FeatureId).HasColumnName("FeatureID");
            entity.Property(e => e.F1).HasMaxLength(100);
            entity.Property(e => e.F2).HasMaxLength(100);
            entity.Property(e => e.F3).HasMaxLength(100);
            entity.Property(e => e.FeatureName).HasMaxLength(200);
        });

        modelBuilder.Entity<LanguageVersion>(entity =>
        {
            entity.HasKey(e => e.VersionId).HasName("PK__Language__16C6402F9672EDAE");

            entity.Property(e => e.VersionId).HasColumnName("VersionID");
            entity.Property(e => e.LanguageId).HasColumnName("LanguageID");
            entity.Property(e => e.Lv1)
                .HasMaxLength(100)
                .HasColumnName("LV1");
            entity.Property(e => e.Lv2)
                .HasMaxLength(100)
                .HasColumnName("LV2");
            entity.Property(e => e.Lv3)
                .HasMaxLength(100)
                .HasColumnName("LV3");
            entity.Property(e => e.VersionName).HasMaxLength(50);

            entity.HasOne(d => d.Language).WithMany(p => p.LanguageVersions)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LanguageV__Langu__48CFD27E");
        });

        modelBuilder.Entity<LearningProgress>(entity =>
        {
            entity.HasKey(e => e.ProgressId).HasName("PK__Learning__BAE29C85A4FAE8CE");

            entity.ToTable("LearningProgress");

            entity.Property(e => e.ProgressId).HasColumnName("ProgressID");
            entity.Property(e => e.Certification).HasMaxLength(200);
            entity.Property(e => e.LanguageId).HasColumnName("LanguageID");
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Lp1)
                .HasMaxLength(100)
                .HasColumnName("LP1");
            entity.Property(e => e.Lp2)
                .HasMaxLength(100)
                .HasColumnName("LP2");
            entity.Property(e => e.Lp3)
                .HasMaxLength(100)
                .HasColumnName("LP3");
            entity.Property(e => e.ProficiencyLevel).HasMaxLength(20);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Language).WithMany(p => p.LearningProgresses)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LearningP__Langu__44FF419A");

            entity.HasOne(d => d.User).WithMany(p => p.LearningProgresses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LearningP__UserI__440B1D61");
        });

        modelBuilder.Entity<LearningResource>(entity =>
        {
            entity.HasKey(e => e.ResourceId).HasName("PK__Learning__4ED1814FAE74E5C2");

            entity.Property(e => e.ResourceId).HasColumnName("ResourceID");
            entity.Property(e => e.LanguageId).HasColumnName("LanguageID");
            entity.Property(e => e.ResourceLink).HasMaxLength(200);
            entity.Property(e => e.ResourceName).HasMaxLength(200);
            entity.Property(e => e.ResourceType).HasMaxLength(50);

            entity.HasOne(d => d.Language).WithMany(p => p.LearningResources)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LearningR__Langu__4BAC3F29");
        });

        modelBuilder.Entity<ProgrammingLanguage>(entity =>
        {
            entity.HasKey(e => e.LanguageId).HasName("PK__Programm__B938558B6D6E3FAD");

            entity.Property(e => e.LanguageId).HasColumnName("LanguageID");
            entity.Property(e => e.Creator).HasMaxLength(100);
            entity.Property(e => e.L1).HasMaxLength(100);
            entity.Property(e => e.L2).HasMaxLength(100);
            entity.Property(e => e.L3).HasMaxLength(100);
            entity.Property(e => e.LanguageName).HasMaxLength(100);
            entity.Property(e => e.OfficialWebsite).HasMaxLength(200);
            entity.Property(e => e.Paradigm).HasMaxLength(200);
        });

        modelBuilder.Entity<ProgrammingLanguageFeature>(entity =>
        {
            entity.HasKey(e => new { e.LanguageId, e.FeatureId }).HasName("PK__Programm__311A6529B7ACAE48");

            entity.Property(e => e.LanguageId).HasColumnName("LanguageID");
            entity.Property(e => e.FeatureId).HasColumnName("FeatureID");
            entity.Property(e => e.Pf1)
                .HasMaxLength(100)
                .HasColumnName("PF1");
            entity.Property(e => e.Pf2)
                .HasMaxLength(100)
                .HasColumnName("PF2");
            entity.Property(e => e.Pf3)
                .HasMaxLength(100)
                .HasColumnName("PF3");

            entity.HasOne(d => d.Feature).WithMany(p => p.ProgrammingLanguageFeatures)
                .HasForeignKey(d => d.FeatureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Programmi__Featu__3E52440B");

            entity.HasOne(d => d.Language).WithMany(p => p.ProgrammingLanguageFeatures)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Programmi__Langu__3D5E1FD2");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC91569775");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.PasswordHash).HasMaxLength(200);
            entity.Property(e => e.RegisterDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Us1)
                .HasMaxLength(100)
                .HasColumnName("US1");
            entity.Property(e => e.Us2)
                .HasMaxLength(100)
                .HasColumnName("US2");
            entity.Property(e => e.Us3)
                .HasMaxLength(100)
                .HasColumnName("US3");
            entity.Property(e => e.UserName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
