using GasStationRatingSystem.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GasStationRatingSystem.DAL
{
   public class GasStationRatingSystemContext:DbContext
    {
        public GasStationRatingSystemContext(DbContextOptions<GasStationRatingSystemContext> options) : base(options) { }

        #region Overrides

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            
            #region People
            modelBuilder.Entity<User>().HasMany(x => x.UserCreated).WithOne(x => x.CreatedUser).HasForeignKey(x => x.AddedBy);
            modelBuilder.Entity<User>().HasMany(x => x.UserModified).WithOne(x => x.ModifiedUser).HasForeignKey(x => x.ModifiedBy);
            modelBuilder.Entity<User>().HasMany(x => x.UserDeleted).WithOne(x => x.DeletedUser).HasForeignKey(x => x.DeletedBy);

            modelBuilder.Entity<User>().HasMany(x => x.UserTypeCreated).WithOne(x => x.CreatedUser).HasForeignKey(x => x.AddedBy);
            modelBuilder.Entity<User>().HasMany(x => x.UserTypeModified).WithOne(x => x.ModifiedUser).HasForeignKey(x => x.ModifiedBy);
            modelBuilder.Entity<User>().HasMany(x => x.UserTypeDeleted).WithOne(x => x.DeletedUser).HasForeignKey(x => x.DeletedBy);



        #endregion


    }

        #endregion
        #region Entities

        #region People

        public DbSet<User> Users { get; set; }



        #endregion

  
        #endregion
    }
}
