using System.Globalization;
using System;
using System.Data.Entity.Migrations;
using System.Linq;
using WEE_WEB_API.Models;
using System.Data.Entity;
using WEE_API.Models;

namespace WEE_WEB_API.Migrations
{


    internal sealed class Configuration : DbMigrationsConfiguration<DBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(DBContext context)
        {

            context.AD_User_Menu.RemoveRange(context.AD_User_Menu.ToList());
            context.AD_Menu.RemoveRange(context.AD_Menu.ToList());
            context.AD_User.RemoveRange(context.AD_User.ToList());




            context.Company.RemoveRange(context.Company.ToList());
            context.CompanyJob.RemoveRange(context.CompanyJob.ToList());
            context.Job.RemoveRange(context.Job.ToList());
            context.Question.RemoveRange(context.Question.ToList());
            context.QuestionType.RemoveRange(context.QuestionType.ToList());
            context.Zone.RemoveRange(context.Zone.ToList());
            context.Location.RemoveRange(context.Location.ToList());


            context.SaveChanges();

            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('AD_AuditLog', RESEED, 0)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('AD_History', RESEED, 0)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('AD_Menu', RESEED, 0)");


            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Company', RESEED, 0)");

            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Job', RESEED, 0)");

            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Question', RESEED, 0)");

            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Zone', RESEED, 0)");

            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Location', RESEED, 0)"); 

            context.AD_User.AddOrUpdate(a => a.UserID, new AD_User { UserID = "Admin", Password = "admin" });
            context.AD_Menu.AddOrUpdate(a => a.MenuID
                , new AD_Menu { MenuID = 30, MenuParentID = null, MenuText = "TRỢ GIÚP", URLAction = "#", MenuIcon = "fa fa-info-circle", MenuSort = 30, MenuSeparator = null, CanDelete = null, }
                , new AD_Menu { MenuID = 31, MenuParentID = null, MenuText = "QUẢN TRỊ", URLAction = "#", MenuIcon = "fa fa-cog", MenuSort = 31, MenuSeparator = null, CanDelete = null, }
                , new AD_Menu { MenuID = 32, MenuParentID = 31, MenuText = "Mã Hàng", URLAction = "/Part", MenuIcon = "fa fa-barcode", MenuSort = 32, MenuSeparator = null, CanDelete = null, }
                , new AD_Menu { MenuID = 33, MenuParentID = 31, MenuText = "Vị Trí", URLAction = "/Location", MenuIcon = "fa fa-flag-o", MenuSort = 33, MenuSeparator = null, CanDelete = null, }
                , new AD_Menu { MenuID = 34, MenuParentID = 31, MenuText = "Khách Hàng", URLAction = "/Customer", MenuIcon = "fa fa-users", MenuSort = 34, MenuSeparator = null, CanDelete = null, }
                , new AD_Menu { MenuID = 35, MenuParentID = 31, MenuText = "Quản lý Menu", URLAction = "/MenuManagement", MenuIcon = "fa fa-user", MenuSort = 35, MenuSeparator = null, CanDelete = null, }
                , new AD_Menu { MenuID = 36, MenuParentID = 31, MenuText = "Người Dùng và Phân Quyền", URLAction = "/Permission", MenuIcon = "fa fa-user", MenuSort = 35, MenuSeparator = null, CanDelete = null, }
            );

            context.Company.AddOrUpdate(a => a.CompanyID,
                new Company {CompanyID=1, CompanyName = "CodeLove" },
                new Company { CompanyID = 2, CompanyName = "RunSystem" }
                );
            context.SaveChanges();

            context.Job.AddOrUpdate(a => a.JobID,
                new Job { JobID=1, JobName = "Lập trình viên" },
                new Job { JobID = 2, JobName = "Designer" }
                );
            context.SaveChanges();

            context.JobType.AddOrUpdate(a => a.JobTypeID,
               new JobType { JobTypeID=1, JobTypeName = "C#" },
               new JobType { JobTypeID = 2, JobTypeName = "Java" }
               );
            context.SaveChanges();

            context.Question.AddOrUpdate(a => a.QuestionID,
               new Question { QuestionID =1, QuestionName = "Nơi bạn làm việc tuân thủ pháp luật KÉM NHẤT ở lĩnh vực nào" },
               new Question { QuestionID =2, QuestionName = "Bạn KHÓ có thể đàm phán nhất ở khía cạnh nào" }
               );
            context.SaveChanges();

            context.Zone.AddOrUpdate(a => a.ZoneID,
             new Zone {ZoneID=1, ZoneName = "C#" },
             new Zone { ZoneID = 2, ZoneName = "Java" }
             );

            context.SaveChanges();


            context.UserType.AddOrUpdate(a => a.UserTypeID,
         new UserType { UserTypeID = 1, UserTypeName = "Người đi làm" },
         new UserType { UserTypeID = 2, UserTypeName = "Chủ doanh nghiệp" },
         new UserType { UserTypeID = 2, UserTypeName = "Bên thứ ba" }
         );

            context.SaveChanges();


            //context.QuestionType.AddOrUpdate(
            //    //a=> new { a.ConpanyID, a.QuestionID, a.TypeID, a.UserID},
            //new QuestionType { ConpanyID = 1, QuestionID = 1, TypeID =1, UserID="Admin", Score=2.0m }            
            //);
            //context.SaveChanges();


            //context.CompanyJob.AddOrUpdate( 
            //new CompanyJob { CompanyID = 1, JobID = 1},
            //new CompanyJob { CompanyID = 1, JobID = 2} ,
            // new CompanyJob { CompanyID = 2, JobID = 1 },
            //new CompanyJob { CompanyID = 2, JobID = 2 }
            //);
            //context.SaveChanges();
        }
    }
}
