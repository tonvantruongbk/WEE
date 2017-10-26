using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Text;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using WEE_API.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using WEE_API.Migrations;
using WEE_API.RBAC;

namespace WEE_API.Models
{

    public class DBContext : IdentityDbContext<ApplicationUser, ApplicationRole, int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public DBContext() : base("name=DBConnectionString")
        {
            Database.SetInitializer(new DBInitializer());
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }
        public virtual DbSet<AD_AuditLog> AD_AuditLog { get; set; }
        public virtual DbSet<AD_History> AD_History { get; set; }
        public virtual DbSet<AD_Menu> AD_Menu { get; set; }
        public virtual DbSet<AD_User> AD_User { get; set; }
        public virtual DbSet<AD_User_Menu> AD_User_Menu { get; set; }


        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Job> Job { get; set; }
        public virtual DbSet<JobType> JobType { get; set; }
        public virtual DbSet<UserRatingCompany> UserRatingCompany { get; set; }
        public virtual DbSet<CompanyZone> CompanyZone { get; set; }
        public virtual DbSet<Zone> Zone { get; set; }

        public virtual DbSet<QuestionType> QuestionType { get; set; }
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<Answer> Answer { get; set; }
        public virtual DbSet<AnswerDetail> AnswerDetail { get; set; }

        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<UserType> UserType { get; set; }
        public virtual DbSet<WorkingStatus> WorkingStatus { get; set; }
        public virtual DbSet<WorkingTime> WorkingTime { get; set; }
        public virtual DbSet<JobPosition> JobPosition { get; set; }
        public virtual DbSet<SalaryLevel> SalaryLevel { get; set; }
        public virtual DbSet<ContractType> ContractType { get; set; }
        public virtual DbSet<TermAndCondition> TermAndCondition { get; set; }



        public DbSet<PERMISSION> PERMISSIONS { get; set; } 

        //public DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().ToTable("USERS").Property(p => p.Id).HasColumnName("UserId");
            modelBuilder.Entity<ApplicationRole>().ToTable("ROLES").Property(p => p.Id).HasColumnName("RoleId");
            modelBuilder.Entity<ApplicationUserRole>().ToTable("LNK_USER_ROLE");

            modelBuilder.Entity<ApplicationRole>().
                HasMany(c => c.PERMISSIONS).
                WithMany(p => p.ROLES).
                Map(
                    m =>
                    {
                        m.MapLeftKey("RoleId");
                        m.MapRightKey("PermissionId");
                        m.ToTable("LNK_ROLE_PERMISSION");
                    });
        }

        public static DBContext Create()
        {
            return new DBContext();
        }

        public override int SaveChanges()
        {

            try
            {
                return base.SaveChanges();
            }
            //catch (Exception ex)
            //{
            //    throw new Exception(
            //        "Entity Validation Failed - errors follow:\n" +
            //         ex
            //    );
            //}
            catch (DbUpdateException e)
            {
                var sb = new StringBuilder();
                sb.AppendLine($"DbUpdateException error details - {e?.InnerException?.InnerException?.Message}");

                foreach (var eve in e.Entries)
                {
                    sb.AppendLine($"Entity of type {eve.Entity.GetType().Name} in state {eve.State} could not be updated");
                }

                var aaa = sb.ToString();

                throw new Exception(sb.ToString());
            }
            //catch (DbEntityValidationException ex)
            //{
            //    var sb = new StringBuilder();

            //    foreach (var failure in ex.EntityValidationErrors)
            //    {
            //        sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
            //        foreach (var error in failure.ValidationErrors)
            //        {
            //            sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
            //            sb.AppendLine();
            //        }
            //    }

            //    throw new DbEntityValidationException(
            //        "Entity Validation Failed - errors follow:\n" +
            //        sb.ToString(), ex
            //    ); // Add the original exception as the innerException
            //}
        }

    }
}
