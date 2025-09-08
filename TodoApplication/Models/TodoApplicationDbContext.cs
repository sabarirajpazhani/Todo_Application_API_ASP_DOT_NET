using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TodoApplication.Models;

public partial class TodoApplicationDbContext : DbContext
{
    public TodoApplicationDbContext()
    {
    }

    public TodoApplicationDbContext(DbContextOptions<TodoApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Todo> Todos { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=BSD-SABARIP01\\SQLEXPRESS;Initial Catalog=todoApplicationDB;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Todo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__todos__3213E83FE34CF81A");

            entity.ToTable("todos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IsComplete)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("false")
                .HasColumnName("isComplete");
            entity.Property(e => e.Title)
                .HasMaxLength(90)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
