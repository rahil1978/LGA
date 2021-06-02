using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Lga.Id.Core.Entities.ScoreAggregate;

namespace Lga.Id.Infrastructure.Data.EntityConfigurations
{
    public class ScoreDetailConfiguration : IEntityTypeConfiguration<ScoreDetail>
    {
        public void Configure(EntityTypeBuilder<ScoreDetail> builder)
        {
            //builder.Property(l => l.Id);                    

            builder.Property(l => l.AdvantageDisadvantageDecile);                    

            builder.Property(l => l.DisadvantageDecile);

            builder.Property(l => l.IndexOfEconomicResourcesScore);
           
            builder.Property(e => e.IndexOfEconomicResourcesDecile);

            builder.Property(e => e.IndexOfEducationAndOccupationScore);

            builder.Property(e => e.IndexOfEducationAndOccupationDecile);

            builder.Property(e => e.UsualResedantPopulation)
                .HasColumnType("decimal(5, 2)");

            builder.Property(e => e.IsDeleted)
                .HasDefaultValue(0);
        }
    }
}
