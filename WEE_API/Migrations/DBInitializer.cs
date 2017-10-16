using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using WEE_API.Models;
using WEE_API.RBAC;
using DBContext = WEE_API.Models.DBContext;

namespace WEE_API.Migrations
{
    public class DBInitializer : DropCreateDatabaseIfModelChanges<DBContext>
    {

        private readonly string c_SysAdmin = "System Administrator";
        private readonly string c_DefaultUser = "Default User";

        protected override void Seed(DBContext context)
        {
            context.AD_User.AddOrUpdate(a => a.UserID, new AD_User { UserID = "Admin", Password = "admin" });

            context.AD_Menu.AddOrUpdate(a => a.MenuID
                , new AD_Menu { MenuID = 1, MenuParentID = null, MenuText = "API", URLAction = "/Swagger", MenuIcon = "fa fa-info-circle", MenuSort = 1, MenuSeparator = null, CanDelete = null, }
                , new AD_Menu { MenuID = 2, MenuParentID = null, MenuText = "DANH MỤC", URLAction = "#", MenuIcon = "fa fa-info-circle", MenuSort = 2, MenuSeparator = null, CanDelete = null, }
                , new AD_Menu { MenuID = 21, MenuParentID = 2, MenuText = "Công ty", URLAction = "/Company", MenuIcon = "fa fa-info-circle", MenuSort = 3, MenuSeparator = null, CanDelete = null, }
                , new AD_Menu { MenuID = 22, MenuParentID = 2, MenuText = "Công việc", URLAction = "/Job", MenuIcon = "fa fa-info-circle", MenuSort = 4, MenuSeparator = null, CanDelete = null, }
                , new AD_Menu { MenuID = 23, MenuParentID = 2, MenuText = "Loại công việc", URLAction = "/JobType", MenuIcon = "fa fa-info-circle", MenuSort = 5, MenuSeparator = null, CanDelete = null, }
                , new AD_Menu { MenuID = 24, MenuParentID = 2, MenuText = "Câu hỏi", URLAction = "/Question", MenuIcon = "fa fa-info-circle", MenuSort = 6, MenuSeparator = null, CanDelete = null, }
                , new AD_Menu { MenuID = 25, MenuParentID = 2, MenuText = "Phân loại người dùng", URLAction = "/UserType", MenuIcon = "fa fa-info-circle", MenuSort = 7, MenuSeparator = null, CanDelete = null, }
                , new AD_Menu { MenuID = 26, MenuParentID = 2, MenuText = "Vị trí", URLAction = "/Location", MenuIcon = "fa fa-info-circle", MenuSort = 8, MenuSeparator = null, CanDelete = null, }
                , new AD_Menu { MenuID = 27, MenuParentID = 2, MenuText = "Lĩnh vực", URLAction = "/Zone", MenuIcon = "fa fa-info-circle", MenuSort = 9, MenuSeparator = null, CanDelete = null, }
                , new AD_Menu { MenuID = 28, MenuParentID = 2, MenuText = "Người dùng Bình chọn", URLAction = "/UserRatingCompany", MenuIcon = "fa fa-info-circle", MenuSort = 10, MenuSeparator = null, CanDelete = null, }
                , new AD_Menu { MenuID = 29, MenuParentID = 2, MenuText = "Tuyển dụng", URLAction = "/CompanyJob", MenuIcon = "fa fa-info-circle", MenuSort = 11, MenuSeparator = null, CanDelete = null, }
                , new AD_Menu { MenuID = 9, MenuParentID = null, MenuText = "QUẢN TRỊ", URLAction = "#", MenuIcon = "fa fa-cog", MenuSort = 31, MenuSeparator = null, CanDelete = null, }
                , new AD_Menu { MenuID = 91, MenuParentID = 9, MenuText = "Quản lý Menu", URLAction = "/MenuManagement", MenuIcon = "fa fa-user", MenuSort = 35, MenuSeparator = null, CanDelete = null, }
                , new AD_Menu { MenuID = 92, MenuParentID = 9, MenuText = "Người Dùng và Phân Quyền", URLAction = "/Permission", MenuIcon = "fa fa-user", MenuSort = 35, MenuSeparator = null, CanDelete = null, }
                , new AD_Menu { MenuID = 100, MenuParentID = null, MenuText = "TRỢ GIÚP", URLAction = "#", MenuIcon = "fa fa-info-circle", MenuSort = 30, MenuSeparator = null, CanDelete = null, }
            );

            context.Location.AddOrUpdate(a => a.LocationID,
                   new Location { LocationID = 1, LocationName = "Hà Nội" },
                   new Location { LocationID = 2, LocationName = "TP. HCM" }
               );


            context.Job.AddOrUpdate(a => a.JobID,
                    new Job { JobID = 1, JobName = "Lập trình viên" },
                    new Job { JobID = 2, JobName = "Designer" }
                );

            context.JobType.AddOrUpdate(a => a.JobTypeID,
                   new JobType { JobTypeID = 1, JobTypeName = "C#" },
                   new JobType { JobTypeID = 2, JobTypeName = "Java" }
               );

            context.Question.AddOrUpdate(a => a.QuestionID,
                   new Question { QuestionID = 1, QuestionName = "Nơi bạn làm việc tuân thủ pháp luật KÉM NHẤT ở lĩnh vực nào" },
                   new Question { QuestionID = 2, QuestionName = "Bạn KHÓ có thể đàm phán nhất ở khía cạnh nào" }
               );

            context.Zone.AddOrUpdate(a => a.ZoneID,
                 new Zone { ZoneID = 1, ZoneName = "C#" },
                 new Zone { ZoneID = 2, ZoneName = "Java" }
             );


            context.Company.AddOrUpdate(a => a.CompanyID,
                  new Company { CompanyID = 1, CompanyName = "CodeLove", LocationID = 1, ZoneID = 1 },
                  new Company { CompanyID = 2, CompanyName = "RunSystem", LocationID = 1, ZoneID = 1 }
              );

            context.UserType.AddOrUpdate(a => a.UserTypeID,
                 new UserType { UserTypeID = 1, UserTypeName = "Người đi làm" },
                 new UserType { UserTypeID = 2, UserTypeName = "Chủ doanh nghiệp" },
                 new UserType { UserTypeID = 3, UserTypeName = "Bên thứ ba" }
             );



            context.QuestionType.AddOrUpdate(a => a.QuestionTypeID,
                 new QuestionType { QuestionTypeID = 1, QuestionTypeName = "Cơ hội việc làm" },
                 new QuestionType { QuestionTypeID = 2, QuestionTypeName = "Lương & Phúc lợi" },
                 new QuestionType { QuestionTypeID = 3, QuestionTypeName = "An toàn" },
                 new QuestionType { QuestionTypeID = 4, QuestionTypeName = "Tổ chức" },
                 new QuestionType { QuestionTypeID = 5, QuestionTypeName = "Khác" }
             );


            context.UserRatingCompany.AddOrUpdate(a => new { a.CompanyID, a.QuestionID, a.QuestionTypeID, a.UserID },
                new UserRatingCompany { CompanyID = 1, QuestionID = 1, QuestionTypeID = 1, UserID = "Admin", Score = 2.0m }
                );

            context.CompanyJob.AddOrUpdate(
                 a => new { a.CompanyID, a.JobID },
                new CompanyJob { CompanyID = 1, JobID = 1 },
                new CompanyJob { CompanyID = 1, JobID = 2 },
                 new CompanyJob { CompanyID = 2, JobID = 1 },
                new CompanyJob { CompanyID = 2, JobID = 2 }
            );
            context.SaveChanges();

            //db.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT AD_Menu ON");
            //db.SaveChanges();
            //db.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT AD_Menu OFF");





            //Create Default Roles...
            IList<ApplicationRole> defaultRoles = new List<ApplicationRole>();
            defaultRoles.Add(new ApplicationRole { Name = c_SysAdmin, RoleDescription = "Allows system administration of Users/Roles/Permissions", LastModified = DateTime.Now, IsSysAdmin = true });
            defaultRoles.Add(new ApplicationRole { Name = c_DefaultUser, RoleDescription = "Default role with limited permissions", LastModified = DateTime.Now, IsSysAdmin = false });

            ApplicationRoleManager RoleManager = new ApplicationRoleManager(new ApplicationRoleStore(context));
            foreach (ApplicationRole role in defaultRoles)
            {
                RoleManager.Create(role);
            }

            //Create User...
            var user = new ApplicationUser { UserName = "Admin", Email = "admin@somedomain.com", Firstname = "System", Lastname = "Administrator", LastModified = DateTime.Now, Inactive = false, EmailConfirmed = true };

            ApplicationUserManager UserManager = new ApplicationUserManager(new ApplicationUserStore(context));
            var result = UserManager.Create(user, "Pa55w0rd");

            if (result.Succeeded)
            {
                //Add User to Admin Role...
                UserManager.AddToRole(user.Id, c_SysAdmin);
            }


            //Create Default User...
            user = new ApplicationUser { UserName = "DefaultUser", Email = "defaultuser@somedomain.com", Firstname = "Default", Lastname = "User", LastModified = DateTime.Now, Inactive = false, EmailConfirmed = true };
            result = UserManager.Create(user, "S4l3su53r");

            if (result.Succeeded)
            {
                //Add User to Admin Role...
                UserManager.AddToRole(user.Id, c_DefaultUser);
            }

            //Create User with NO Roles...
            user = new ApplicationUser { UserName = "Guest", Email = "guest@somedomain.com", Firstname = "Guest", Lastname = "User", LastModified = DateTime.Now, Inactive = false, EmailConfirmed = true };
            result = UserManager.Create(user, "Gu3st12");


            base.Seed(context);

            //Create a permission...
            PERMISSION _permission = new PERMISSION { PermissionDescription = "Home-Reports" };
            ApplicationRoleManager.AddPermission(_permission);

            //Add Permission to DefaultUser Role...
            ApplicationRoleManager.AddPermission2Role(context.Roles.First(p => p.Name == c_DefaultUser).Id, context.PERMISSIONS.First().PermissionId);




            base.Seed(context);
        }
    }
}