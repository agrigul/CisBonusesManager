﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Web.Models.Bonuses;

namespace Web.Infrastructure.Mapping
{
    /// <summary>
    /// mapping class of BonusAggregate entity to database
    /// </summary>
    public class BonusMapping : EntityTypeConfiguration<BonusAggregate>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BonusMapping"/> class.
        /// </summary>
        public BonusMapping()
        {
            ToTable("CompDev.vwBonuses");
            //ToTable("vwBonuses");// NOTE: uncomment this line for local database testgs

            HasKey(x => x.BonusId);
            Property(x => x.BonusId)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.EmployeeId); // link to another view
            Property(x => x.Date);
            Property(x => x.Amount);
            Property(x => x.Comment);
            Property(x => x.IsActive).HasColumnName("Active");
            Property(x => x.Ulc).HasColumnName("ULC");
            Property(x => x.Dlc).HasColumnName("DLC");
        }
    }
}