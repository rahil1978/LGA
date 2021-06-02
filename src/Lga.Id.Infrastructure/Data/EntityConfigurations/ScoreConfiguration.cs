using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Lga.Id.Core.Entities.ScoreAggregate;


namespace Lga.Id.Infrastructure.Data.EntityConfigurations
{
    public class ScoreConfiguration : IEntityTypeConfiguration<Score>
    {
        public void Configure(EntityTypeBuilder<Score> builder)
        {
           

            builder.Property(l => l.DisadvantageScore);

            builder.Property(l => l.AdvantageDisadvantageScore)
                .HasDefaultValue(0); ;

            builder.Property(l => l.Year)
                .IsRequired();

            builder.Property(l => l.Location.Id);

        }
    }
}
