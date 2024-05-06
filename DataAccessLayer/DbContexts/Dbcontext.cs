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

    public virtual DbSet<LeaveType> LeaveTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserType> UserTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }


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
