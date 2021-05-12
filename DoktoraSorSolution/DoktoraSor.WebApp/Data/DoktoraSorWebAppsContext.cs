using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DoktoraSor.WebApp.Data
{
    public class DoktoraSorWebAppsContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public DoktoraSorWebAppsContext() : base("name=DoktoraSorWebAppsContext")
        {
        }

        public System.Data.Entity.DbSet<DoktoraSor.Entities.Product> Products { get; set; }

        public System.Data.Entity.DbSet<DoktoraSor.Entities.Sales> Sales { get; set; }

        public System.Data.Entity.DbSet<DoktoraSor.Entities.Department> Departments { get; set; }

        public System.Data.Entity.DbSet<DoktoraSor.Entities.Expertise> Expertises { get; set; }

        public System.Data.Entity.DbSet<DoktoraSor.Entities.Degree> Degrees { get; set; }

        public System.Data.Entity.DbSet<DoktoraSor.Entities.Note> Notes { get; set; }

        public System.Data.Entity.DbSet<DoktoraSor.Entities.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<DoktoraSor.Entities.DoctorUser> DoctorUsers { get; set; }

        public System.Data.Entity.DbSet<DoktoraSor.Entities.PatientUser> PatientUsers { get; set; }
    }
}
