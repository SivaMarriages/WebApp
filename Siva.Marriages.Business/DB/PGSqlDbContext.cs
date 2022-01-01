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
        public virtual DbSet<ProfilePicture> ProfilePictures { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profile>(entity =>
            {
                entity.ToTable("profiles");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Json)
                    .HasColumnType("json")
                    .HasColumnName("json");
            });

            modelBuilder.Entity<ProfilePicture>(entity =>
            {
                entity.ToTable("profilePictures");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ProfileId).HasColumnName("profileId");

                entity.HasOne(d => d.Profile)
                    .WithMany(p => p.ProfilePictures)
                    .HasForeignKey(d => d.ProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("profilePicturesKey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
