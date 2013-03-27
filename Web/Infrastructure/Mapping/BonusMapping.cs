using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Web.Models;

namespace Web.Infrastructure.Mapping
{
    /// <summary>
    /// mapping class to Bonus entity to database
    /// </summary>
    public class BonusMapping : EntityTypeConfiguration<Bonus>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BonusMapping"/> class.
        /// </summary>
        public BonusMapping()
        {
            ToTable("CompDev.vwBonuses");
            
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(x => x.Employee)
                .WithOptional()
                .WillCascadeOnDelete(false);
            Property(x => x.Date);
            Property(x => x.Amount);
            Property(x => x.Comment);
            Property(x => x.IsActive).HasColumnName("Active");
            Property(x => x.Ulc).HasColumnName("ULC");
            Property(x => x.Dlc).HasColumnName("DLC");
        }
    }
}