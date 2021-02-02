using Lizy.TerritorialDivisionService.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lizy.TerritorialDivisionService.Data.EntityConfiguration
{
    /// <summary>
    /// This isn't complete implementation as EF throws warnings
    /// But for test purposes this shall do
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class DivisionConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity: Division
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            var valueConverter = new ValueConverter<List<Coordinates>,string> (
                v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                v => JsonConvert.DeserializeObject<List<Coordinates>>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })
            );
            builder
                .Property(e => e.Coordinates)
                .HasConversion(valueConverter, new ValueComparer<List<int>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList())
                );

            builder.OwnsOne(e => e.Penetration,( penetrationBuilder ) =>
            {
                var name = nameof(Penetration);
                penetrationBuilder.Property(p => p.Value).HasColumnName($"{name}");
                penetrationBuilder.Property(p => p.PercentileFrom).HasColumnName($"{name}{nameof(Penetration.PercentileFrom)}");
                penetrationBuilder.Property(p => p.PercentileTill).HasColumnName($"{name}{nameof(Penetration.PercentileTill)}");
            });
        }
    }
}
