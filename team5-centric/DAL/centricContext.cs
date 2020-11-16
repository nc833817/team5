using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using team5_centric.Models;

namespace team5_centric.DAL
{
    public class centricContext : DbContext
    {
        public centricContext() : base ("name=DefaultConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<centricContext, team5_centric.Migrations.centricContext.Configuration>("DefaultConnection"));
        }
    
        public DbSet<values> values { get; set; }
        public DbSet<recognition> recognitions { get; set; }
        public DbSet<userData> userDatas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
            
        }
    }
        
}