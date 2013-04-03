using System.Data.Entity;
using Web.Infrastructure.Mapping;
using Web.Models;

namespace Web.Infrastructure.Repository
{
    /// <summary>
    /// Database context class
    /// </summary>
    public class DatabaseContext : DbContext
    {
        /// <summary>
        /// The name connection to database
        /// </summary>
        private const string ConnectionStringName = "BonusesDbContext";

        /// <summary>
        /// Gets or sets the employees table context.
        /// </summary>
        /// <value>The employees.</value>
        public DbSet<Employee> Employees { get; set; }

        /// <summary>
        /// Gets or sets the bonuses table context
        /// </summary>
        /// <value>The bonuses.</value>
        public DbSet<Bonus> Bonuses { get; set; }

        /// <summary>
        /// Sets database initialization strategy
        /// </summary>
        public DatabaseContext()
            : base(ConnectionStringName)
        {
            Database.SetInitializer<DatabaseContext>(null); // without creating new database
        }

        /// <summary>
        /// Set the mapping configuration
        /// </summary>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BonusMapping());
            modelBuilder.Configurations.Add(new EmployeeMapping());
            Configuration.LazyLoadingEnabled = false;
            base.OnModelCreating(modelBuilder);
        }
    }
}