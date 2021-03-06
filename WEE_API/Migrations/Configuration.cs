﻿using System.Globalization;
using System;
using System.Data.Entity.Migrations;
using System.Linq;
using WEE_API.Models;
using System.Data.Entity;

namespace WEE_API.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
        //protected override void Seed(DBContext context)
        //{
        //    ////context.AD_User_Menu.RemoveRange(context.AD_User_Menu.ToList());
        //    ////context.AD_Menu.RemoveRange(context.AD_Menu.ToList());
        //    ////context.AD_User.RemoveRange(context.AD_User.ToList());

        //    ////context.Company.RemoveRange(context.Company.ToList());
        //    ////context.CompanyJob.RemoveRange(context.CompanyJob.ToList());
        //    ////context.Job.RemoveRange(context.Job.ToList());
        //    ////context.Question.RemoveRange(context.Question.ToList());
        //    ////context.UserRatingCompany.RemoveRange(context.UserRatingCompany.ToList());
        //    ////context.Zone.RemoveRange(context.Zone.ToList());
        //    ////context.Location.RemoveRange(context.Location.ToList());
        //    ////context.QuestionType.RemoveRange(context.QuestionType.ToList());

        //    ////context.SaveChanges();

        //    ////context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('AD_AuditLog', RESEED, 1)");
        //    ////context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('AD_History', RESEED, 1)");

        //    ////context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Company', RESEED, 1)");
        //    ////context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Job', RESEED, 1)");
        //    ////context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Question', RESEED, 1)");
        //    ////context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Zone', RESEED, 1)");
        //    ////context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Location', RESEED, 1)");
        //    ////context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('QuestionType', RESEED, 1)");

        //    //context.AD_User.AddOrUpdate(a => a.UserID, new AD_User { UserID = "Admin", Password = "admin" });
        //    //context.SaveChanges();

        //    //context.AD_Menu.AddOrUpdate(a => a.MenuID
        //    //    , new AD_Menu { MenuID = 1, MenuParentID = null, MenuText = "API", URLAction = "/Swagger", MenuIcon = "fa fa-info-circle", MenuSort = 1, MenuSeparator = null, CanDelete = null, }
        //    //    , new AD_Menu { MenuID = 2, MenuParentID = null, MenuText = "DANH MỤC", URLAction = "#", MenuIcon = "fa fa-info-circle", MenuSort = 2, MenuSeparator = null, CanDelete = null, }
        //    //    , new AD_Menu { MenuID = 21, MenuParentID = 2, MenuText = "Công ty", URLAction = "/Company", MenuIcon = "fa fa-info-circle", MenuSort = 3, MenuSeparator = null, CanDelete = null, }
        //    //    , new AD_Menu { MenuID = 22, MenuParentID = 2, MenuText = "Công việc", URLAction = "/Job", MenuIcon = "fa fa-info-circle", MenuSort = 4, MenuSeparator = null, CanDelete = null, }
        //    //    , new AD_Menu { MenuID = 23, MenuParentID = 2, MenuText = "Loại công việc", URLAction = "/JobType", MenuIcon = "fa fa-info-circle", MenuSort = 5, MenuSeparator = null, CanDelete = null, }
        //    //    , new AD_Menu { MenuID = 24, MenuParentID = 2, MenuText = "Câu hỏi", URLAction = "/Question", MenuIcon = "fa fa-info-circle", MenuSort = 6, MenuSeparator = null, CanDelete = null, }
        //    //    , new AD_Menu { MenuID = 25, MenuParentID = 2, MenuText = "Phân loại người dùng", URLAction = "/UserType", MenuIcon = "fa fa-info-circle", MenuSort = 7, MenuSeparator = null, CanDelete = null, }
        //    //    , new AD_Menu { MenuID = 26, MenuParentID = 2, MenuText = "Vị trí", URLAction = "/Location", MenuIcon = "fa fa-info-circle", MenuSort = 8, MenuSeparator = null, CanDelete = null, }
        //    //    , new AD_Menu { MenuID = 27, MenuParentID = 2, MenuText = "Lĩnh vực", URLAction = "/Zone", MenuIcon = "fa fa-info-circle", MenuSort = 9, MenuSeparator = null, CanDelete = null, }
        //    //    , new AD_Menu { MenuID = 28, MenuParentID = 2, MenuText = "Người dùng Bình chọn", URLAction = "/UserRatingCompany", MenuIcon = "fa fa-info-circle", MenuSort = 10, MenuSeparator = null, CanDelete = null, }
        //    //    , new AD_Menu { MenuID = 29, MenuParentID = 2, MenuText = "Tuyển dụng", URLAction = "/CompanyJob", MenuIcon = "fa fa-info-circle", MenuSort = 11, MenuSeparator = null, CanDelete = null, }
        //    //    , new AD_Menu { MenuID = 9, MenuParentID = null, MenuText = "QUẢN TRỊ", URLAction = "#", MenuIcon = "fa fa-cog", MenuSort = 31, MenuSeparator = null, CanDelete = null, }
        //    //    , new AD_Menu { MenuID = 91, MenuParentID = 9, MenuText = "Quản lý Menu", URLAction = "/MenuManagement", MenuIcon = "fa fa-user", MenuSort = 35, MenuSeparator = null, CanDelete = null, }
        //    //    , new AD_Menu { MenuID = 92, MenuParentID = 9, MenuText = "Người Dùng và Phân Quyền", URLAction = "/Permission", MenuIcon = "fa fa-user", MenuSort = 35, MenuSeparator = null, CanDelete = null, }
        //    //    , new AD_Menu { MenuID = 100, MenuParentID = null, MenuText = "TRỢ GIÚP", URLAction = "#", MenuIcon = "fa fa-info-circle", MenuSort = 30, MenuSeparator = null, CanDelete = null, }
        //    //);
        //    //context.SaveChanges();

        //    //context.Location.AddOrUpdate(a => a.LocationID,
        //    //       new Location { LocationID = 1, LocationName = "Hà Nội" },
        //    //       new Location { LocationID = 2, LocationName = "TP. HCM" }
        //    //   );
        //    //context.SaveChanges();


        //    //context.Job.AddOrUpdate(a => a.JobID,
        //    //        new Job { JobID = 1, JobName = "Lập trình viên" },
        //    //        new Job { JobID = 2, JobName = "Designer" }
        //    //    );
        //    //context.SaveChanges();

        //    //context.JobType.AddOrUpdate(a => a.JobTypeID,
        //    //       new JobType { JobTypeID = 1, JobTypeName = "C#" },
        //    //       new JobType { JobTypeID = 2, JobTypeName = "Java" }
        //    //   );
        //    //context.SaveChanges();

        //    //context.Question.AddOrUpdate(a => a.QuestionID,
        //    //       new Question { QuestionID = 1, QuestionName = "Nơi bạn làm việc tuân thủ pháp luật KÉM NHẤT ở lĩnh vực nào" },
        //    //       new Question { QuestionID = 2, QuestionName = "Bạn KHÓ có thể đàm phán nhất ở khía cạnh nào" }
        //    //   );
        //    //context.SaveChanges();

        //    //context.Zone.AddOrUpdate(a => a.ZoneID,
        //    //     new Zone { ZoneID = 1, ZoneName = "C#" },
        //    //     new Zone { ZoneID = 2, ZoneName = "Java" }
        //    // );

        //    //context.SaveChanges();

        //    //context.Company.AddOrUpdate(a => a.CompanyID,
        //    //      new Company { CompanyID = 1, CompanyName = "CodeLove", LocationID = 1, ZoneID = 1 },
        //    //      new Company { CompanyID = 2, CompanyName = "RunSystem", LocationID = 1, ZoneID = 1 }
        //    //  );
        //    //context.SaveChanges();

        //    //context.UserType.AddOrUpdate(a => a.UserTypeID,
        //    //     new UserType { UserTypeID = 1, UserTypeName = "Người đi làm" },
        //    //     new UserType { UserTypeID = 2, UserTypeName = "Chủ doanh nghiệp" },
        //    //     new UserType { UserTypeID = 3, UserTypeName = "Bên thứ ba" }
        //    // );

        //    //context.SaveChanges();


        //    //context.QuestionType.AddOrUpdate(a => a.QuestionTypeID,
        //    //     new QuestionType { QuestionTypeID = 1, QuestionTypeName = "Cơ hội việc làm" },
        //    //     new QuestionType { QuestionTypeID = 2, QuestionTypeName = "Lương & Phúc lợi" },
        //    //     new QuestionType { QuestionTypeID = 3, QuestionTypeName = "An toàn" },
        //    //     new QuestionType { QuestionTypeID = 4, QuestionTypeName = "Tổ chức" },
        //    //     new QuestionType { QuestionTypeID = 5, QuestionTypeName = "Khác" }
        //    // );

        //    //context.SaveChanges();

        //    //context.UserRatingCompany.AddOrUpdate(a => new { a.CompanyID, a.QuestionID, a.QuestionTypeID, a.UserID },
        //    //    new UserRatingCompany { CompanyID = 1, QuestionID = 1, QuestionTypeID = 1, UserID = "Admin", Score = 2.0m }
        //    //    );
        //    //context.SaveChanges();

        //    //context.CompanyJob.AddOrUpdate(
        //    //     a => new { a.CompanyID, a.JobID},
        //    //    new CompanyJob { CompanyID = 1, JobID = 1 },
        //    //    new CompanyJob { CompanyID = 1, JobID = 2 },
        //    //     new CompanyJob { CompanyID =2, JobID = 1 },
        //    //    new CompanyJob { CompanyID = 2, JobID = 2 }
        //    //);
        //    //context.SaveChanges();

        //    //db.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT AD_Menu ON");
        //    //db.SaveChanges();
        //    //db.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT AD_Menu OFF");
        //}
    }
}
