using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Siva.Marriages.Business.DB.Models;

namespace Siva.Marriages.Business.DB
{
    public partial class PGSqlDbContext : DbContext
    {
        public PGSqlDbContext()
        {
        }

        public PGSqlDbContext(DbContextOptions<PGSqlDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Profile> Profiles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profile>(entity =>
            {
                entity.ToTable("profiles");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Data)
                    .HasColumnType("json")
                    .HasColumnName("data");

                entity.Property(e => e.Pictures)
                    .HasColumnType("json")
                    .HasColumnName("pictures");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
