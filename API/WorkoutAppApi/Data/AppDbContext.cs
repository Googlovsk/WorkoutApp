using Microsoft.EntityFrameworkCore;
using Schedule.Models.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Schedule.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Lession> Lessions { get; set; } // Занятия
        public DbSet<LessionStudent> LessionStudents { get; set; } // Связь студенты-занятия
        public DbSet<Category> Categories { get; set; } // Категории занятий
        public DbSet<Schedule.Models.Domain.Schedule> Schedules { get; set; } // Расписание
        public DbSet<Location> Locations { get; set; } // Кабинеты


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Настройка связей "многие-ко-многим" для LessionStudent
            modelBuilder.Entity<LessionStudent>()
                .HasKey(ls => new { ls.LessionId, ls.StudentId });

            modelBuilder.Entity<LessionStudent>()
                .HasOne(ls => ls.Lession)
                .WithMany(l => l.Students)
                .HasForeignKey(ls => ls.LessionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LessionStudent>()
                .HasOne(ls => ls.Student)
                .WithMany()
                .HasForeignKey(ls => ls.StudentId)
                .IsRequired();

            // Настройка отношений для Schedule
            modelBuilder.Entity<Schedule.Models.Domain.Schedule>()
                .HasOne(s => s.Teacher)
                .WithMany()
                .HasForeignKey(s => s.TeacherId);

            modelBuilder.Entity<Schedule.Models.Domain.Schedule>()
                .HasOne(s => s.Location)
                .WithMany(r => r.Schedules)
                .HasForeignKey(s => s.LocationId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Schedule.Models.Domain.Schedule>()
                .HasOne(s => s.Lession)
                .WithMany()
                .HasForeignKey(s => s.LessionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
