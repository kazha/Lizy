using Lizy.TerritorialDivisionService.Data.EntityConfiguration;
using Lizy.TerritorialDivisionService.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lizy.TerritorialDivisionService.Data
{
    public class RegionDbContext: DbContext
    {
        public DbSet<SquareKilometer> SquareKilometers { get; set; }
        public DbSet<Parish> Parishes { get; set; }
        public DbSet<County> Counties { get; set; }

        public RegionDbContext(DbContextOptions<RegionDbContext> options)
            :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DivisionConfiguration<SquareKilometer>());
            modelBuilder.ApplyConfiguration(new DivisionConfiguration<Parish>());
            modelBuilder.ApplyConfiguration(new DivisionConfiguration<County>());
            base.OnModelCreating(modelBuilder);
        }
    }
}
