using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaret.DAL
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ETicaretContext>
    {
        public ETicaretContext CreateDbContext(string[] args)
        {
            var builder=new DbContextOptionsBuilder<ETicaretContext>();
            builder.UseSqlServer("Server=.;Database=ETicaretDB;Trusted_Connection=True;");
            return new ETicaretContext(builder.Options);
        }
    }
}
