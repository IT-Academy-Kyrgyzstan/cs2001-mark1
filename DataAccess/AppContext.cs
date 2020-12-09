using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{   
    public class AppContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        
        public AppContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
            => builder.UseSqlServer(@"");
    }
}
