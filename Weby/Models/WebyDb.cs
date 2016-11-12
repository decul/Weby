using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Weby.Models
{
    public class WebyDb : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Day> Days { get; set; }


        public WebyDb() : base("name=DefaultConnection") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Add missing fields of User to table "dbo.AspNetUsers" instead of creating new table
            modelBuilder.Entity<User>().ToTable("dbo.AspNetUsers");
        }
    }
}