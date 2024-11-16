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

    public virtual DbSet<TblProduct> TblProducts { get; set; }

    public virtual DbSet<TblProductCategory> TblProductCategories { get; set; }

    public virtual DbSet<TblSaleInvoice> TblSaleInvoices { get; set; }

    public virtual DbSet<TblSaleInvoiceDetail> TblSaleInvoiceDetails { get; set; }

    public virtual DbSet<TblShop> TblShops { get; set; }

    public virtual DbSet<TblStaff> TblStaffs { get; set; }

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

        modelBuilder.Entity<TblProduct>(entity =>
        {
            entity.HasKey(e => e.ProductId);

            entity.ToTable("Tbl_Product");

            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ProductCategoryCode).HasMaxLength(50);
            entity.Property(e => e.ProductCode).HasMaxLength(50);
            entity.Property(e => e.ProductName).HasMaxLength(50);
        });

        modelBuilder.Entity<TblProductCategory>(entity =>
        {
            entity.HasKey(e => e.ProductCategoryId);

            entity.ToTable("Tbl_ProductCategory");

            entity.Property(e => e.ProductCategoryCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProductCategoryName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblSaleInvoice>(entity =>
        {
            entity.HasKey(e => e.SaleInvoiceId);

            entity.ToTable("Tbl_SaleInvoice");

            entity.Property(e => e.Change).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CustomerAccountNo).HasMaxLength(20);
            entity.Property(e => e.CustomerCode).HasMaxLength(50);
            entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PaymentAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PaymentType).HasMaxLength(10);
            entity.Property(e => e.ReceiveAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SaleInvoiceDateTime).HasColumnType("datetime");
            entity.Property(e => e.StaffCode).HasMaxLength(50);
            entity.Property(e => e.Tax).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.VoucherNo).HasMaxLength(20);
        });

        modelBuilder.Entity<TblSaleInvoiceDetail>(entity =>
        {
            entity.HasKey(e => e.SaleInvoiceDetailId);

            entity.ToTable("Tbl_SaleInvoiceDetail");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ProductCode).HasMaxLength(50);
            entity.Property(e => e.VoucherNo).HasMaxLength(20);
        });

        modelBuilder.Entity<TblShop>(entity =>
        {
            entity.HasKey(e => e.ShopId);

            entity.ToTable("Tbl_Shop");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MobileNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ShopCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ShopName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblStaff>(entity =>
        {
            entity.HasKey(e => e.StaffId);

            entity.ToTable("Tbl_Staff");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MobileNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Position)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StaffCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StaffName)
                .HasMaxLength(50)
                .IsUnicode(false);
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
