using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Web.Models;

namespace Web.Infrastructure.Mapping
{
    /// <summary>
    /// Mapping class of  Employee entity to database
    /// </summary>
    public class EmployeeMapping : EntityTypeConfiguration<Employee>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeMapping"/> class.
        /// </summary>
        public EmployeeMapping()
        {
            ToTable("vwEmployeesLookup");

            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("EmployeeID")
                               .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.UserName).IsRequired();
            Property(x => x.LastName).IsRequired();
            Property(x => x.LastNameUkr).IsRequired();
        }
    }
}