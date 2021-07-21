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

            #region Guide
            modelBuilder.Entity<User>().HasMany(x => x.GasStationCreated).WithOne(x => x.CreatedUser).HasForeignKey(x => x.AddedBy);
            modelBuilder.Entity<User>().HasMany(x => x.GasStationModified).WithOne(x => x.ModifiedUser).HasForeignKey(x => x.ModifiedBy);
            modelBuilder.Entity<User>().HasMany(x => x.GasStationDeleted).WithOne(x => x.DeletedUser).HasForeignKey(x => x.DeletedBy);

            modelBuilder.Entity<User>().HasMany(x => x.GasStationContactCreated).WithOne(x => x.CreatedUser).HasForeignKey(x => x.AddedBy);
            modelBuilder.Entity<User>().HasMany(x => x.GasStationContactModified).WithOne(x => x.ModifiedUser).HasForeignKey(x => x.ModifiedBy);
            modelBuilder.Entity<User>().HasMany(x => x.GasStationContactDeleted).WithOne(x => x.DeletedUser).HasForeignKey(x => x.DeletedBy);
            #region Questions
            modelBuilder.Entity<User>().HasMany(x => x.QuestionCreated).WithOne(x => x.CreatedUser).HasForeignKey(x => x.AddedBy);
            modelBuilder.Entity<User>().HasMany(x => x.QuestionModified).WithOne(x => x.ModifiedUser).HasForeignKey(x => x.ModifiedBy);
            modelBuilder.Entity<User>().HasMany(x => x.QuestionDeleted).WithOne(x => x.DeletedUser).HasForeignKey(x => x.DeletedBy);
           
            modelBuilder.Entity<User>().HasMany(x => x.AnswerCategoryCreated).WithOne(x => x.CreatedUser).HasForeignKey(x => x.AddedBy);
            modelBuilder.Entity<User>().HasMany(x => x.AnswerCategoryModified).WithOne(x => x.ModifiedUser).HasForeignKey(x => x.ModifiedBy);
            modelBuilder.Entity<User>().HasMany(x => x.AnswerCategoryDeleted).WithOne(x => x.DeletedUser).HasForeignKey(x => x.DeletedBy);

            modelBuilder.Entity<User>().HasMany(x => x.AnswerCreated).WithOne(x => x.CreatedUser).HasForeignKey(x => x.AddedBy);
            modelBuilder.Entity<User>().HasMany(x => x.AnswerModified).WithOne(x => x.ModifiedUser).HasForeignKey(x => x.ModifiedBy);
            modelBuilder.Entity<User>().HasMany(x => x.AnswerDeleted).WithOne(x => x.DeletedUser).HasForeignKey(x => x.DeletedBy);


            #endregion
            #endregion
            #region Visit
            modelBuilder.Entity<User>().HasMany(x => x.VisitInfoCreated).WithOne(x => x.CreatedUser).HasForeignKey(x => x.AddedBy);
            modelBuilder.Entity<User>().HasMany(x => x.VisitInfoModified).WithOne(x => x.ModifiedUser).HasForeignKey(x => x.ModifiedBy);
            modelBuilder.Entity<User>().HasMany(x => x.VisitInfoDeleted).WithOne(x => x.DeletedUser).HasForeignKey(x => x.DeletedBy);


            #endregion
           
        }

        #endregion
        #region Entities

        #region People

        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }





        #endregion

        #region Guide
        public DbSet<GasStation> GasStations { get; set; }
        public DbSet<GasStationContact> GasStationContacts { get; set; }
        #region Questions
        public DbSet<Question> Questions { get; set; }
        public DbSet<AnswerCategory> AnswerCategories { get; set; }
        public DbSet<Answer> Answers { get; set; }


        #endregion
        #endregion
        #endregion
    }
}
