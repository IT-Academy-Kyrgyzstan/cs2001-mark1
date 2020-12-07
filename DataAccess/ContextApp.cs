using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    class ContextApp : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ContextApp()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
            => builder.UseSqlServer(@"Server=DORON-PC\SQLEXPRESS; Database=Mark1; Trusted_Connection=True;");
    }
}
