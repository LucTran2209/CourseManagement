using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CMS_API.Models
{
    public partial class PROJECT_PRN231Context : DbContext
    {
        public PROJECT_PRN231Context()
        {
        }

        public PROJECT_PRN231Context(DbContextOptions<PROJECT_PRN231Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<ContentDetail> ContentDetails { get; set; } = null!;
        public virtual DbSet<ContentType> ContentTypes { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<CourseContent> CourseContents { get; set; } = null!;
        public virtual DbSet<File> Files { get; set; } = null!;
        public virtual DbSet<Folder> Folders { get; set; } = null!;
        public virtual DbSet<PrivateFile> PrivateFiles { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserJoinCourse> UserJoinCourses { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server =(local); database =PROJECT_PRN231 ;uid=sa;pwd=123456;Trusted_Connection=True;Encrypt=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ContentDetail>(entity =>
            {
                entity.ToTable("ContentDetail");

                entity.Property(e => e.ContentDetailId).ValueGeneratedNever();

                entity.Property(e => e.Detail).IsUnicode(false);

                entity.Property(e => e.TimeUpLoad)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Title).HasMaxLength(150);
            });

            modelBuilder.Entity<ContentType>(entity =>
            {
                entity.ToTable("ContentType");

                entity.Property(e => e.ContentTypeName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(e => e.CourseTitle)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Semester)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TimeEnd).HasColumnType("date");

                entity.Property(e => e.TimeStart).HasColumnType("date");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Courses_Categories");
            });

            modelBuilder.Entity<CourseContent>(entity =>
            {
                entity.ToTable("CourseContent");

                entity.Property(e => e.Content).IsUnicode(false);

                entity.Property(e => e.ContentTitle).HasMaxLength(150);

                entity.Property(e => e.TimeUpLoad).HasColumnType("datetime");

                entity.HasOne(d => d.ContentType)
                    .WithMany(p => p.CourseContents)
                    .HasForeignKey(d => d.ContentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseContent_ContentType");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseContents)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseContent_Courses");
            });

            modelBuilder.Entity<File>(entity =>
            {
                entity.ToTable("File");

                entity.Property(e => e.ContentFile).IsUnicode(false);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.HasOne(d => d.Folder)
                    .WithMany(p => p.Files)
                    .HasForeignKey(d => d.FolderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_File_Folder");
            });

            modelBuilder.Entity<Folder>(entity =>
            {
                entity.ToTable("Folder");

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.HasOne(d => d.CourseContent)
                    .WithMany(p => p.Folders)
                    .HasForeignKey(d => d.CourseContentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Folder_CourseContent");
            });

            modelBuilder.Entity<PrivateFile>(entity =>
            {
                entity.HasKey(e => new { e.FileId, e.UserId });

                entity.ToTable("PrivateFile");

                entity.Property(e => e.Content)
                    .IsUnicode(false)
                    .HasColumnName("content");

                entity.HasOne(d => d.File)
                    .WithMany(p => p.PrivateFiles)
                    .HasForeignKey(d => d.FileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PrivateFile_File");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PrivateFiles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PrivateFile_Users");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleTitle)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Country).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.GoogleId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Role");
            });

            modelBuilder.Entity<UserJoinCourse>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.CourseId });

                entity.ToTable("UserJoinCourse");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TimeJoin).HasColumnType("datetime");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.UserJoinCourses)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserJoinCourse_Courses");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserJoinCourses)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserJoinCourse_Users");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
