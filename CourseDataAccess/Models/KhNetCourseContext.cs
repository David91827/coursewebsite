﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CourseDataAccess.Models;

public partial class KhNetCourseContext : DbContext
{
    public KhNetCourseContext(DbContextOptions<KhNetCourseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Courseschedule> Courseschedules { get; set; }

    public virtual DbSet<Stucourseschedule> Stucourseschedules { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Sysadmin> Sysadmins { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.Entity<Course>(entity =>
        {
            entity.ToTable("course");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("code");
            entity.Property(e => e.Description)
                .HasComment("填課程說明")
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Times).HasColumnName("times");
        });

        modelBuilder.Entity<Courseschedule>(entity =>
        {
            entity.ToTable("courseschedule");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Courseid).HasColumnName("courseid");
            entity.Property(e => e.Edate).HasColumnName("edate");
            entity.Property(e => e.Location)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("location");
            entity.Property(e => e.Sdate).HasColumnName("sdate");
            entity.Property(e => e.Teacherid).HasColumnName("teacherid");

            entity.HasOne(d => d.Course).WithMany(p => p.Courseschedules)
                .HasForeignKey(d => d.Courseid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_courseschedule_course");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Courseschedules)
                .HasForeignKey(d => d.Teacherid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_courseschedule_teacher");
        });

        modelBuilder.Entity<Stucourseschedule>(entity =>
        {
            entity.ToTable("stucourseschedule");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Cscheduleid).HasColumnName("cscheduleid");
            entity.Property(e => e.Studentid).HasColumnName("studentid");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.ToTable("student");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Mobile)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("mobile");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("password");
        });

        modelBuilder.Entity<Sysadmin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_user");

            entity.ToTable("sysadmin");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Initdate)
                .HasColumnType("datetime")
                .HasColumnName("initdate");
            entity.Property(e => e.Modifydate)
                .HasColumnType("datetime")
                .HasColumnName("modifydate");
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.ToTable("teacher");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Mobile)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("mobile");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("name");
        });
        modelBuilder.HasSequence("MySeq");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}