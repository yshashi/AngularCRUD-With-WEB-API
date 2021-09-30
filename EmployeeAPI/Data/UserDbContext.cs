using EmployeeAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext>options):base(options)
        {

        }
        public DbSet<UserModel> userModels { get; set; }
        public DbSet<EmployeeModel> employeeModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeModel>().ToTable("tbl_employee");
            modelBuilder.Entity<UserModel>().ToTable("tbl_user");
        }
    }
}
