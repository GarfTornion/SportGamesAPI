using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SportGamesAPI.Models;

public partial class SportGameDbContext : DbContext
{
    public SportGameDbContext()
    {
    }

    public SportGameDbContext(DbContextOptions<SportGameDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<SportGame> SportGames { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SportGame>(entity =>
        {
            entity.ToTable("SportGame");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EndTime)
                .HasColumnType("datetime")
                .HasColumnName("endTime");
            entity.Property(e => e.Finished)
                .HasDefaultValueSql("((0))")
                .HasColumnName("finished");
            entity.Property(e => e.StartTime)
                .HasColumnType("datetime")
                .HasColumnName("startTime");
            entity.Property(e => e.Team1Name)
                .HasMaxLength(50)
                .HasColumnName("team1Name");
            entity.Property(e => e.Team1Score).HasColumnName("team1Score");
            entity.Property(e => e.Team2Name)
                .HasMaxLength(50)
                .HasColumnName("team2Name");
            entity.Property(e => e.Team2Score).HasColumnName("team2Score");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
