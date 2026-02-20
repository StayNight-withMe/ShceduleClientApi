

using Domain.Model.Entities;
using Domain.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataBase.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<DayScheduleEntity> DaySchedule { get; set; }
        public DbSet<LessonEntity> Lesson { get; set; }
        public DbSet<TeacherEntity> Teacher { get; set; }
        public DbSet<GroupEntity> Group { get; set; }
        public DbSet<SpecialtyEntity> Specialty { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LessonEntity>()
          .HasIndex(e => new { e.Teacher1, e.Teacher2 })
          .HasDatabaseName("Teachers_Index");

            modelBuilder.Entity<DayScheduleEntity>()
            .HasIndex(e => new { e.Date })
            .HasDatabaseName("Date_Index");

            modelBuilder.Entity<DayScheduleEntity>()
            .HasIndex(e => new { e.GroupName })
            .HasDatabaseName("Group_Index");

        }
    }
}
