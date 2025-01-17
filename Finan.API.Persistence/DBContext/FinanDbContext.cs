using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finan.API.Domain.Estudents;
using Microsoft.EntityFrameworkCore;

namespace Finan.API.Persistence.DBContext
{
    public class FinanDbContext : DbContext
    {
        public FinanDbContext(DbContextOptions<FinanDbContext> options) : base(options) { }

        public DbSet<StudentsEntity> Estudents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de mapeos usando Fluent API
            modelBuilder.Entity<StudentsEntity>(entity =>
            {
                entity.ToTable("Students");
                entity.HasKey(r => r.Id);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
