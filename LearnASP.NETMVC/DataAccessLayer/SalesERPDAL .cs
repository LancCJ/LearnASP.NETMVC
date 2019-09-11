using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using LearnASP.NETMVC.Models;

namespace LearnASP.NETMVC.DataAccessLayer
{
    public class SalesERPDAL: DbContext
    {



        public SalesERPDAL() : base("NewName")
     {
     }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("TblEmployee");
            base.OnModelCreating(modelBuilder);
        }



        public DbSet<Employee> Employees { get; set; }

    }

   
}