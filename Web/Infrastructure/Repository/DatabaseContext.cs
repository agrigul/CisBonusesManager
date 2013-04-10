using System;
using System.Data.Entity;
using Web.Infrastructure.Mapping;
using Web.Models;
using Web.Models.Bonuses;

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
        public DbSet<BonusAggregate> Bonuses { get; set; }

        /// <summary>
        /// Sets database initialization strategy
        /// </summary>
        public DatabaseContext()
            : base(ConnectionStringName)
        {
            Database.SetInitializer<DatabaseContext>(null); // without creating new database
            
            var user = SessionRepository.CurrentSession.UserCredentials as UserCredentials;
            //TODO: delete hardcode below
            if(user == null)
                throw new ArgumentNullException("DatabaseContext", "UserCredentials object can not be null");

            string connectionString = string.Format("{0} User ID={1};Password={2}",
                                                   System.Configuration.ConfigurationManager.
                                                     ConnectionStrings[ConnectionStringName].ConnectionString,
                                                   user.Login,
                                                   user.Password
                                                   );
            this.Database.Connection.ConnectionString = connectionString;

        }


        public DatabaseContext(string login, string password)
            : base(ConnectionStringName)
        {
            Database.SetInitializer<DatabaseContext>(null); // without creating new database
            
            string connectionString = string.Format("{0} User ID={1};Password={2}",
                                                    System.Configuration.ConfigurationManager.
                                                      ConnectionStrings[ConnectionStringName].ConnectionString,
                                                    login,
                                                    password
                                                    );

            this.Database.Connection.ConnectionString = connectionString;
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