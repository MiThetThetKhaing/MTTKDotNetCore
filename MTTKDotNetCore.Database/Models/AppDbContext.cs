using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MTTKDotNetCore.Database.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TaskCategory> TaskCategories { get; set; }

    public virtual DbSet<TblAccount> TblAccounts { get; set; }

    public virtual DbSet<TblBlog> TblBlogs { get; set; }

    public virtual DbSet<TblTransactionHistory> TblTransactionHistories { get; set; }

    public virtual DbSet<ToDoList> ToDoLists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-6QTP69L\\MSSQLSERVER2022;Database=DotNetTrainingBatch5;User Id=sa;Password=sa123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__TaskCate__19093A2B910431A2");

            entity.ToTable("TaskCategory");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblAccount>(entity =>
        {
            entity.ToTable("Tbl_Account");

            entity.Property(e => e.Balance).HasColumnType("decimal(25, 2)");
            entity.Property(e => e.FullName).HasMaxLength(150);
            entity.Property(e => e.MobileNo).HasMaxLength(50);
            entity.Property(e => e.Pin)
                .HasMaxLength(6)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblBlog>(entity =>
        {
            entity.HasKey(e => e.BlogId);

            entity.ToTable("Tbl_Blog");

            entity.Property(e => e.BlogAuthor).HasMaxLength(50);
            entity.Property(e => e.BlogTitle).HasMaxLength(50);
        });

        modelBuilder.Entity<TblTransactionHistory>(entity =>
        {
            entity.HasKey(e => e.TranId);

            entity.ToTable("Tbl_TransactionHistory");

            entity.Property(e => e.Amount).HasColumnType("decimal(25, 2)");
            entity.Property(e => e.FromMobileNo).HasMaxLength(50);
            entity.Property(e => e.Notes).HasMaxLength(150);
            entity.Property(e => e.ToMobileNo).HasMaxLength(50);
            entity.Property(e => e.TranDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<ToDoList>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("PK__ToDoList__7C6949D1850D9B9F");

            entity.ToTable("ToDoList");

            entity.Property(e => e.TaskId).HasColumnName("TaskID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CompletedDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DueDate).HasColumnType("date");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TaskDescription).HasColumnType("text");
            entity.Property(e => e.TaskTitle)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Category).WithMany(p => p.ToDoLists)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__ToDoList__Catego__5441852A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
