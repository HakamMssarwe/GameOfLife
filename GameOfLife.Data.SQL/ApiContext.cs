using GameOfLife.Infrastructure.Entities.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Data.SQL
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cell>()
                .HasKey(x => new { x.RowId, x.ColumnId });
        }

        public DbSet<Cell> Cells { get; set; }

    }
}
