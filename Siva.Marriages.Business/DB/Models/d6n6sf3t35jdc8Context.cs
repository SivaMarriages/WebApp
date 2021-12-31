using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Siva.Marriages.Business.DB.Models
{
    public partial class d6n6sf3t35jdc8Context : DbContext
    {
        public d6n6sf3t35jdc8Context()
        {
        }

        public d6n6sf3t35jdc8Context(DbContextOptions<d6n6sf3t35jdc8Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Profile> Profiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profile>(entity =>
            {
                entity.ToTable("profiles");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Json)
                    .IsRequired()
                    .HasColumnType("json")
                    .HasColumnName("json");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
