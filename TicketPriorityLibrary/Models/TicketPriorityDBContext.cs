using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TicketPriorityLibrary.Models;

public partial class TicketPriorityDBContext : DbContext
{
    public TicketPriorityDBContext()
    {
    }

    public TicketPriorityDBContext(DbContextOptions<TicketPriorityDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TicketPriority> TicketPriorities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source = (localdb)\\MSSQLLocalDB; database = TicketPriorityDB; integrated security = true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TicketPriority>(entity =>
        {
            entity.HasKey(e => e.PriorityId).HasName("PK__TicketPr__D0A3D0BEB914B4E7");

            entity.ToTable("TicketPriority");

            entity.Property(e => e.PriorityId).ValueGeneratedNever();
            entity.Property(e => e.PriorityName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
