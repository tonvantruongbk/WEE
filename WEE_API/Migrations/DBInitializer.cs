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

        public override void InitializeDatabase(DBContext context)
        {
            if (context.Database.Exists())
            {
                context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction,
               string.Format("ALTER DATABASE {0} SET SINGLE_USER WITH ROLLBACK IMMEDIATE", context.Database.Connection.Database));
            }
            base.InitializeDatabase(context);
            if (context.Database.Exists())
            {
                context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction,
                    string.Format("ALTER DATABASE {0} SET MULTI_USER WITH ROLLBACK IMMEDIATE", context.Database.Connection.Database));
            }
        }

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

            #region Location
            context.Location.AddOrUpdate(a => a.LocationID,
                        new Location { LocationID = 1, LocationName = "An Giang" },
                        new Location { LocationID = 2, LocationName = "Bà Rịa - Vũng Tàu" },
                        new Location { LocationID = 3, LocationName = "Bạc Liêu" },
                        new Location { LocationID = 4, LocationName = "Bắc Kạn" },
                        new Location { LocationID = 5, LocationName = "Bắc Giang" },
                        new Location { LocationID = 6, LocationName = "Bắc Ninh" },
                        new Location { LocationID = 7, LocationName = "Bến Tre" },
                        new Location { LocationID = 8, LocationName = "Bình Dương" },
                        new Location { LocationID = 9, LocationName = "Bình Định" },
                        new Location { LocationID = 10, LocationName = "Bình Phước" },
                        new Location { LocationID = 11, LocationName = "Bình Thuận" },
                        new Location { LocationID = 12, LocationName = "Cà Mau" },
                        new Location { LocationID = 13, LocationName = "Cao Bằng" },
                        new Location { LocationID = 14, LocationName = "Cần Thơ" },
                        new Location { LocationID = 15, LocationName = "Đà Nẵng" },
                        new Location { LocationID = 16, LocationName = "Đắk Lắk" },
                        new Location { LocationID = 17, LocationName = "Đắk Nông" },
                        new Location { LocationID = 18, LocationName = "Đồng Nai" },
                        new Location { LocationID = 19, LocationName = "Đồng Tháp" },
                        new Location { LocationID = 20, LocationName = "Điện Biên" },
                        new Location { LocationID = 21, LocationName = "Gia Lai" },
                        new Location { LocationID = 22, LocationName = "Hà Giang" },
                        new Location { LocationID = 23, LocationName = "Hà Nam" },
                        new Location { LocationID = 24, LocationName = "Hà Nội" },
                        new Location { LocationID = 25, LocationName = "Hà Tĩnh" },
                        new Location { LocationID = 26, LocationName = "Hải Dương" },
                        new Location { LocationID = 27, LocationName = "Hải Phòng" },
                        new Location { LocationID = 28, LocationName = "Hòa Bình" },
                        new Location { LocationID = 29, LocationName = "Hậu Giang" },
                        new Location { LocationID = 30, LocationName = "Hưng Yên" },
                        new Location { LocationID = 31, LocationName = "Thành phố Hồ Chí Minh" },
                        new Location { LocationID = 32, LocationName = "Khánh Hòa" },
                        new Location { LocationID = 33, LocationName = "Kiên Giang" },
                        new Location { LocationID = 34, LocationName = "Kon Tum" },
                        new Location { LocationID = 35, LocationName = "Lai Châu" },
                        new Location { LocationID = 36, LocationName = "Lào Cai" },
                        new Location { LocationID = 37, LocationName = "Lạng Sơn" },
                        new Location { LocationID = 38, LocationName = "Lâm Đồng" },
                        new Location { LocationID = 39, LocationName = "Long An" },
                        new Location { LocationID = 40, LocationName = "Nam Định" },
                        new Location { LocationID = 41, LocationName = "Nghệ An" },
                        new Location { LocationID = 42, LocationName = "Ninh Bình" },
                        new Location { LocationID = 43, LocationName = "Ninh Thuận" },
                        new Location { LocationID = 44, LocationName = "Phú Thọ" },
                        new Location { LocationID = 45, LocationName = "Phú Yên" },
                        new Location { LocationID = 46, LocationName = "Quảng Bình" },
                        new Location { LocationID = 47, LocationName = "Quảng Nam" },
                        new Location { LocationID = 48, LocationName = "Quảng Ngãi" },
                        new Location { LocationID = 49, LocationName = "Quảng Ninh" },
                        new Location { LocationID = 50, LocationName = "Quảng Trị" },
                        new Location { LocationID = 51, LocationName = "Sóc Trăng" },
                        new Location { LocationID = 52, LocationName = "Sơn La" },
                        new Location { LocationID = 53, LocationName = "Tây Ninh" },
                        new Location { LocationID = 54, LocationName = "Thái Bình" },
                        new Location { LocationID = 55, LocationName = "Thái Nguyên" },
                        new Location { LocationID = 56, LocationName = "Thanh Hóa" },
                        new Location { LocationID = 57, LocationName = "Thừa Thiên - Huế" },
                        new Location { LocationID = 58, LocationName = "Tiền Giang" },
                        new Location { LocationID = 59, LocationName = "Trà Vinh" },
                        new Location { LocationID = 60, LocationName = "Tuyên Quang" },
                        new Location { LocationID = 61, LocationName = "Vĩnh Long" },
                        new Location { LocationID = 62, LocationName = "Vĩnh Phúc" },
                        new Location { LocationID = 63, LocationName = "Yên Bái" }
                   );

            #endregion

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

            #region Zone
            context.Zone.AddOrUpdate(a => a.ZoneID,
                     new Zone { ZoneID = 1, ZoneName = "Nhân viên kinh doanh" },
                     new Zone { ZoneID = 2, ZoneName = "Hành chính-Văn phòng" },
                     new Zone { ZoneID = 3, ZoneName = "Kế toán-Kiểm toán" },
                     new Zone { ZoneID = 4, ZoneName = "Bán hàng" },
                     new Zone { ZoneID = 5, ZoneName = "Marketing-PR" },
                     new Zone { ZoneID = 6, ZoneName = "Tư vấn" },
                     new Zone { ZoneID = 7, ZoneName = "KD bất động sản" },
                     new Zone { ZoneID = 8, ZoneName = "Xây dựng" },
                     new Zone { ZoneID = 9, ZoneName = "IT phần mềm" },
                     new Zone { ZoneID = 10, ZoneName = "Điện-Điện tử" },
                     new Zone { ZoneID = 11, ZoneName = "Y tế-Dược" },
                     new Zone { ZoneID = 12, ZoneName = "Cơ khí-Chế tạo" },
                     new Zone { ZoneID = 13, ZoneName = "Kỹ thuật" },
                     new Zone { ZoneID = 14, ZoneName = "Kiến trúc-TK nội thất" },
                     new Zone { ZoneID = 15, ZoneName = "Nhân sự" },
                     new Zone { ZoneID = 16, ZoneName = "Biên-Phiên dịch" },
                     new Zone { ZoneID = 17, ZoneName = "Giáo dục-Đào tạo" },
                     new Zone { ZoneID = 18, ZoneName = "Quản trị kinh doanh" },
                     new Zone { ZoneID = 19, ZoneName = "Xuất, nhập khẩu" },
                     new Zone { ZoneID = 20, ZoneName = "IT phần cứng/mạng" },
                     new Zone { ZoneID = 21, ZoneName = "Thiết kế-Mỹ thuật" },
                     new Zone { ZoneID = 22, ZoneName = "Khách sạn-Nhà hàng" },
                     new Zone { ZoneID = 23, ZoneName = "Thư ký-Trợ lý" },
                     new Zone { ZoneID = 24, ZoneName = "Dịch vụ" },
                     new Zone { ZoneID = 25, ZoneName = "Điện tử viễn thông" },
                     new Zone { ZoneID = 26, ZoneName = "Thiết kế đồ hoạ web" },
                     new Zone { ZoneID = 27, ZoneName = "Dệt may - Da giày" },
                     new Zone { ZoneID = 28, ZoneName = "Tiếp thị-Quảng cáo" },
                     new Zone { ZoneID = 29, ZoneName = "Thương mại điện tử" },
                     new Zone { ZoneID = 30, ZoneName = "Vật tư-Thiết bị" },
                     new Zone { ZoneID = 31, ZoneName = "Kỹ thuật ứng dụng" },
                     new Zone { ZoneID = 32, ZoneName = "Báo chí-Truyền hình" },
                     new Zone { ZoneID = 33, ZoneName = "Ngành nghề khác" },
                     new Zone { ZoneID = 34, ZoneName = "Ngân hàng" },
                     new Zone { ZoneID = 35, ZoneName = "Thực phẩm-Đồ uống" },
                     new Zone { ZoneID = 36, ZoneName = "Ô tô - Xe máy" },
                     new Zone { ZoneID = 37, ZoneName = "Du lịch" },
                     new Zone { ZoneID = 38, ZoneName = "Thời trang" },
                     new Zone { ZoneID = 39, ZoneName = "Vận tải" },
                     new Zone { ZoneID = 40, ZoneName = "Bảo hiểm" },
                     new Zone { ZoneID = 41, ZoneName = "Công nghiệp" },
                     new Zone { ZoneID = 42, ZoneName = "Hoá học-Sinh học" },
                     new Zone { ZoneID = 43, ZoneName = "Pháp lý" },
                     new Zone { ZoneID = 44, ZoneName = "In ấn-Xuất bản" },
                     new Zone { ZoneID = 45, ZoneName = "Mỹ phẩm-Trang sức" },
                     new Zone { ZoneID = 46, ZoneName = "Nông-Lâm-Ngư nghiệp" },
                     new Zone { ZoneID = 47, ZoneName = "Quan hệ đối ngoại" },
                     new Zone { ZoneID = 48, ZoneName = "Tổ chức sự kiện-Quà tặng" },
                     new Zone { ZoneID = 49, ZoneName = "Hoạch định-Dự án" },
                     new Zone { ZoneID = 50, ZoneName = "Hàng gia dụng" },
                     new Zone { ZoneID = 51, ZoneName = "Dầu khí-Hóa chất" },
                     new Zone { ZoneID = 52, ZoneName = "Công nghệ cao" },
                     new Zone { ZoneID = 53, ZoneName = "Nghệ thuật - Điện ảnh" },
                     new Zone { ZoneID = 54, ZoneName = "Bưu chính" },
                     new Zone { ZoneID = 55, ZoneName = "Bảo vệ" },
                     new Zone { ZoneID = 56, ZoneName = "Game" },
                     new Zone { ZoneID = 57, ZoneName = "Chứng khoán- Vàng" },
                     new Zone { ZoneID = 58, ZoneName = "Hàng không" },
                     new Zone { ZoneID = 59, ZoneName = "Đầu tư" },
                     new Zone { ZoneID = 60, ZoneName = "Thủ công mỹ nghệ" },
                     new Zone { ZoneID = 61, ZoneName = "Hàng hải" }
              );

            #endregion

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