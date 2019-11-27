using CodingChallenge.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace CodingChallenge.Repository
{
    public partial class CodingChallengeContext : DbContext
    {
        public CodingChallengeContext()
        {
        }

        public CodingChallengeContext(DbContextOptions<CodingChallengeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Dependent> Dependents { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dependent>(entity =>
            {
                entity.ToTable("dependent");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.FirstName)
                    .HasColumnName("first_name")
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasColumnName("last_name")
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("employee_id");

                entity.Property(e => e.DependentTotalCostPerPayPeriod)
                    .HasColumnName("cost_per_pay_period")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.DependentTotalCostPerYear)
                    .HasColumnName("cost_per_year")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.DependentType)
                    .HasColumnName("dependent_type");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Dependents)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_dependent_employee");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employee");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.FirstName)
                    .HasColumnName("first_name")
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasColumnName("last_name")
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeTotalCostPerPayPeriod)
                    .HasColumnName("cost_per_pay_period")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.EmployeeTotalCostPerYear)
                    .HasColumnName("cost_per_year")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.EmployeeAndDependentsTotalCostPerPayPeriod)
                    .HasColumnName("total_cost_per_pay_period")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.EmployeeAndDependentsTotalCostPerYear)
                    .HasColumnName("total_cost_per_year")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PayPerPeriod)
                    .HasColumnName("pay_per_period")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PayPerYear)
                    .HasColumnName("pay_per_year")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.NetPayPerPeriod)
                    .HasColumnName("net_pay_per_period")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.NetPayPerYear)
                    .HasColumnName("net_pay_per_year")
                    .HasDefaultValueSql("((0))");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
