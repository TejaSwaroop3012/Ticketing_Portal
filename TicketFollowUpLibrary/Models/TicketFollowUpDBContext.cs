using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TicketFollowUpLibrary.Models;

public partial class TicketFollowUpDBContext : DbContext
{
    public TicketFollowUpDBContext()
    {
    }

    public TicketFollowUpDBContext(DbContextOptions<TicketFollowUpDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<TicketFollowup> TicketFollowups { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source = (localdb)\\MSSQLLocalDB; database = TicketFollowUpDB; integrated security = true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK__Ticket__712CC60710669A6E");

            entity.ToTable("Ticket");
        });

        modelBuilder.Entity<TicketFollowup>(entity =>
        {
            entity.HasKey(e => new { e.TicketId, e.SrNo }).HasName("PK__TicketFo__AD168B3D7B8A3A33");

            entity.ToTable("TicketFollowup");

            entity.Property(e => e.Remarks)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Ticket).WithMany(p => p.TicketFollowups)
                .HasForeignKey(d => d.TicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TicketFol__Ticke__3D5E1FD2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
