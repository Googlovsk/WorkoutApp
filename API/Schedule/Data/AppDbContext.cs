using Microsoft.EntityFrameworkCore;
using Schedule.Models.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Schedule.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }
        public DbSet<AppUser> Users { get; set; }
        //public DbSet<Role> Roles { get; set; }
        //public DbSet<Category> Categories { get; set; }
        //public DbSet<Lession> Lessions { get; set; }
        //public DbSet<LessionStudent> LessionsStudents { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    // Связь User -> Role (многие к 1)
        //    modelBuilder.Entity<User>()
        //        .HasOne(u => u.Role)
        //        .WithMany(r => r.Users)
        //        .HasForeignKey(u => u.RoleId)
        //        .OnDelete(DeleteBehavior.Restrict);

        //    // Связь User -> Lession (Учитель)
        //    modelBuilder.Entity<Lession>()
        //        .HasOne(l => l.Teacher)
        //        .WithMany(u => u.LessonsAsTeacher)
        //        .HasForeignKey(l => l.TeacherId)
        //        .OnDelete(DeleteBehavior.Restrict);

        //    // Связь Lession -> Category (многие к 1)
        //    modelBuilder.Entity<Lession>()
        //        .HasOne(l => l.Category)
        //        .WithMany(c => c.Lessions)
        //        .HasForeignKey(l => l.CategoryId)
        //        .OnDelete(DeleteBehavior.Restrict);

        //    // Связь Lession -> User через LessionStudent (многие ко многим)
        //    modelBuilder.Entity<LessionStudent>()
        //        .HasKey(ls => new { ls.LessionId, ls.StudentId }); // Составной ключ

        //    modelBuilder.Entity<LessionStudent>()
        //        .HasOne(ls => ls.Lession)
        //        .WithMany(l => l.Students)
        //        .HasForeignKey(ls => ls.LessionId)
        //        .OnDelete(DeleteBehavior.Cascade);

        //    modelBuilder.Entity<LessionStudent>()
        //        .HasOne(ls => ls.Student)
        //        .WithMany(u => u.LessonsAsStudent)
        //        .HasForeignKey(ls => ls.StudentId)
        //        .OnDelete(DeleteBehavior.Cascade);

        //    modelBuilder.Entity<Role>().HasData(
        //        new Role { Id = 1, Name = "Admin" },
        //        new Role { Id = 2, Name = "Преподователь" },
        //        new Role { Id = 3, Name = "Студент" }
        //        );
        //}
    }
}
