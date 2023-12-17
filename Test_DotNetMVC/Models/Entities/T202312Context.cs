using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Test_DotNetMVC.Models.Entities
{
    public partial class T202312Context : DbContext
    {
        public T202312Context()
        {
        }

        public T202312Context(DbContextOptions<T202312Context> options)
            : base(options)
        {
        }

        public virtual DbSet<MSchool> MSchools { get; set; } = null!;
        public virtual DbSet<MSex> MSexes { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MSchool>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("m_Schools");

                entity.Property(e => e.DeleteFlg)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.SchoolId)
                    .HasMaxLength(1)
                    .HasColumnName("SchoolID")
                    .IsFixedLength();

                entity.Property(e => e.SchoolName).HasMaxLength(50);
            });

            modelBuilder.Entity<MSex>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("m_Sex");

                entity.Property(e => e.DeleteFlg)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.SexId)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.SexName).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(250);

                entity.Property(e => e.DeleteFlg)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.Email).HasMaxLength(150);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.School)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.Sex)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
