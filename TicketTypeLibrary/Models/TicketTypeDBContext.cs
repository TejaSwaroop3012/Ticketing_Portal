using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TicketTypeLibrary.Models;

public partial class TicketTypeDBContext : DbContext
{
    public TicketTypeDBContext()
    {
    }

    public TicketTypeDBContext(DbContextOptions<TicketTypeDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<TicketPriority> TicketPriorities { get; set; }

    public virtual DbSet<TicketType> TicketTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source = (localdb)\\MSSQLLocalDB; database = TicketTypeDB; integrated security = true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmpId).HasName("PK__Employee__AF2DBB998B0F53A4");

            entity.ToTable("Employee");

            entity.Property(e => e.EmpId).ValueGeneratedNever();
        });

        modelBuilder.Entity<TicketPriority>(entity =>
        {
            entity.HasKey(e => e.PriorityId).HasName("PK__TicketPr__D0A3D0BE0944BD63");

            entity.ToTable("TicketPriority");

            entity.Property(e => e.PriorityId).ValueGeneratedNever();
        });

        modelBuilder.Entity<TicketType>(entity =>
        {
            entity.HasKey(e => e.TicketTypeId).HasName("PK__TicketTy__6CD684319ADB0E88");

            entity.ToTable("TicketType");

            entity.Property(e => e.TicketTypeId).ValueGeneratedNever();
            entity.Property(e => e.TicketTypeName)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.AssignedToEmp).WithMany(p => p.TicketTypes)
                .HasForeignKey(d => d.AssignedToEmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TicketTyp__Assig__3A81B327");

            entity.HasOne(d => d.Priority).WithMany(p => p.TicketTypes)
                .HasForeignKey(d => d.PriorityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TicketTyp__Prior__3B75D760");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
