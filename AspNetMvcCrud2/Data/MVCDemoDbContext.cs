using AspNetMvcCrud2.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace ASPNETMVCCRUD.Data
{
    public class MVCDemoDbContext : DbContext
    {
        public MVCDemoDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

      
    }
}
