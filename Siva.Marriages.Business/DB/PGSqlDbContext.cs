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

        public virtual DbSet<Business.DB.Models.Profile> Profiles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Business.DB.Models.Profile>(entity =>
            {
                entity.ToTable("profiles");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Json)
                    .HasColumnType("json")
                    .HasColumnName("json");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
