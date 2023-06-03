using ECHO.DB.Models;
using Microsoft.EntityFrameworkCore;


namespace ECHO.DB.Context
{
    public partial class EchoContext : DbContext
    {
        public EchoContext() { }
        public EchoContext(DbContextOptions<EchoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Company> Companies { get; set; } = null!;
        public virtual DbSet<Control> Controls { get; set; } = null!;
        public virtual DbSet<ControlType> ControlTypes { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Method> Methods { get; set; } = null!;
        public virtual DbSet<Procedure> Procedures { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<TimePeriod> TimePeriods { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
               optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=echo;Trusted_Connection=True;TrustServerCertificate=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("companies");
            });

            modelBuilder.Entity<Control>(entity =>
            {
                entity.ToTable("controls");
            });

            modelBuilder.Entity<ControlType>(entity =>
            {
                entity.ToTable("control_types");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employees");
            });

            modelBuilder.Entity<Method>(entity =>
            {
                entity.ToTable("methods");
            });

            modelBuilder.Entity<Procedure>(entity =>
            {
                entity.ToTable("procedures");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");
            });

            modelBuilder.Entity<TimePeriod>(entity =>
            {
                entity.ToTable("time_periods");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
