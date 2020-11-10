using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlayTech.Shared.Database;
using PlayTech.Shared.Database.Interfaces;
using PlayTech.UnitOfWork.Models;

#nullable disable

namespace PlayTech.UnitOfWork
{
    public class PlayTechContext : EfDbContext, IDbContext
    {
        public PlayTechContext(DbContextOptions options) : base(options)
        {
        }

        public PlayTechContext(DbContextOptions<PlayTechContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.ManagerId).HasColumnName("ManagerID");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Salary).HasColumnType("decimal(18, 2)");
            });
        }
    }
}
