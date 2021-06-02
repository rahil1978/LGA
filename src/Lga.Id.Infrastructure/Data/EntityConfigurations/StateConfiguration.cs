using Lga.Id.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lga.Id.Infrastructure.Data.EntityConfigurations
{
    public class StateConfiguration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.Property(l => l.StateName)
                .HasColumnType("varchar")
                .HasMaxLength(50);
        }
    }
}
