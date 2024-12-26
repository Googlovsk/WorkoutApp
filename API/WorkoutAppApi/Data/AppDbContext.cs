using Microsoft.EntityFrameworkCore;
using WorkoutAppApi.Models.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace WorkoutAppApi.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Lession> Lessions { get; set; }
        public DbSet<LessionStudent> LessionStudents { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<TeacherSchedule> Schedules { get; set; }
        public DbSet<Notification> Notifications { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /// AppUser
            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.ToTable("AspNetUsers");
                entity.Property(u => u.Gender)
                    .HasMaxLength(7)
                    .IsRequired(false)
                    .HasColumnType("nvarchar(7)");
                entity.HasMany(u => u.LessionStudents)
                    .WithOne(ls => ls.Student)
                    .HasForeignKey(ls => ls.StudentId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(u => u.Notifications)
                    .WithOne(n => n.Recipient)
                    .HasForeignKey(n => n.RecipientId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            /// Category
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(255);
                entity.HasOne(c => c.ParentCategory)
                    .WithMany(c => c.Subcategories)
                    .HasForeignKey(c => c.ParentCategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            /// Lession
            modelBuilder.Entity<Lession>(entity =>
            {
                entity.HasKey(l => l.Id);
                entity.Property(l => l.Name)
                    .IsRequired()
                    .HasMaxLength(255);
                entity.Property(l => l.Notes)
                    .HasMaxLength(500);
                entity.Property(l => l.Status)
                    .IsRequired();
                entity.HasOne(l => l.Teacher)
                    .WithMany()
                    .HasForeignKey(l => l.TeacherId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(l => l.Category)
                    .WithMany(c => c.Lessions)
                    .HasForeignKey(l => l.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(l => l.Students)
                    .WithOne(ls => ls.Lession)
                    .HasForeignKey(ls => ls.LessionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            /// LessionStudent
            modelBuilder.Entity<LessionStudent>(entity =>
            {
                entity.HasKey(ls => new { ls.LessionId, ls.StudentId });
                entity.HasOne(ls => ls.Lession)
                    .WithMany(l => l.Students)
                    .HasForeignKey(ls => ls.LessionId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(ls => ls.Student)
                    .WithMany(u => u.LessionStudents)
                    .HasForeignKey(ls => ls.StudentId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            /// Notification
            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(n => n.Id);
                entity.Property(n => n.Message)
                    .IsRequired()
                    .HasMaxLength(1000);
                entity.Property(n => n.SentDate)
                    .IsRequired();
                entity.HasOne(n => n.Recipient)
                    .WithMany(u => u.Notifications)
                    .HasForeignKey(n => n.RecipientId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            /// TeacherSchedule
            modelBuilder.Entity<TeacherSchedule>(entity =>
            {
                entity.HasKey(ts => ts.Id);
                entity.Property(ts => ts.CanWorkWith)
                    .IsRequired();
                entity.Property(ts => ts.CanWorkBy)
                    .IsRequired();
                entity.HasOne(ts => ts.Teacher)
                    .WithMany()
                    .HasForeignKey(ts => ts.TeacherId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            ///добавление данных в базу при создании
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "test", ParentCategoryId = null }
                );
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "2", Name = "User", NormalizedName = "USER" },
                new IdentityRole { Id = "3", Name = "Manager", NormalizedName = "MANAGER" }
);
        }
    }
}
