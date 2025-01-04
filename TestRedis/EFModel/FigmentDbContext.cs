using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TestRedis.EFModel;

public partial class FigmentDbContext : DbContext
{
    public FigmentDbContext()
    {
    }

    public FigmentDbContext(DbContextOptions<FigmentDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-N2A120E1\\SQLEXPRESS;Initial Catalog=FigmentDB;Integrated Security=True;Persist Security Info=False;User ID=sa;Password=admin@123; Encrypt=True;TrustServerCertificate=True");


    #region warning
    // To protect potentially sensitive information in your connection string, you should move it out of
    //source code.You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - 
    //see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings,
    //see https://go.microsoft.com/fwlink/?LinkId=723263.

    #endregion
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC0C651239");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105346677B632").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
