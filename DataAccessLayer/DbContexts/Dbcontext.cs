using System;
using System.Collections.Generic;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DbContexts;

public partial class Dbcontext : DbContext
{
    public Dbcontext()
    {
    }

    public Dbcontext(DbContextOptions<Dbcontext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attendence> Attendences { get; set; }

    public virtual DbSet<EmployeePayroll> EmployeePayrolls { get; set; }

    public virtual DbSet<Leave> Leaves { get; set; }

    public virtual DbSet<LeaveStatus> LeaveStatuses { get; set; }

    public virtual DbSet<LeaveType> LeaveTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserType> UserTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-O6HU8I0;Initial Catalog=EmployeeManagementDb;Integrated Security=True;TrustServerCertificate=true ");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attendence>(entity =>
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.TimedIn).HasDefaultValueSql("(getutcdate())");
        });

        modelBuilder.Entity<EmployeePayroll>(entity =>
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Identifier).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<Leave>(entity =>
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Identifier).HasDefaultValueSql("(newid())");
            entity.Property(e => e.LeaveStatusId).HasDefaultValue(1);

            entity.HasOne(d => d.LeaveStatus).WithMany(p => p.Leaves)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Leave__LeaveStat__4AB81AF0");

            entity.HasOne(d => d.LeaveType).WithMany(p => p.Leaves)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Leave__LeaveType__49C3F6B7");
        });

        modelBuilder.Entity<LeaveStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LeaveSta__3214EC07341058D8");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Identifier).HasDefaultValueSql("(newid())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LoggedIn).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.UsertypeId).HasDefaultValue(2);

            entity.HasOne(d => d.Usertype).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__UsertypeI__412EB0B6");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
