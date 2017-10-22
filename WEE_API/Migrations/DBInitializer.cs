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
                , new AD_Menu { MenuID = 1, MenuParentID = null, MenuText = "API", URLAction = "/Swagger", MenuIcon = "fa fa-info-circle", MenuSort = 1, MenuSeparator = null, CanDelete = null }
                , new AD_Menu { MenuID = 2, MenuParentID = null, MenuText = "DANH MỤC", URLAction = "#", MenuIcon = "fa fa-info-circle", MenuSort = 2, MenuSeparator = null, CanDelete = null }
                , new AD_Menu { MenuID = 201, MenuParentID = 2, MenuText = "Công ty", URLAction = "/Company", MenuIcon = "fa fa-info-circle", MenuSort = 3, MenuSeparator = null, CanDelete = null }
                , new AD_Menu { MenuID = 202, MenuParentID = 2, MenuText = "Công việc", URLAction = "/Job", MenuIcon = "fa fa-info-circle", MenuSort = 4, MenuSeparator = null, CanDelete = null }
                , new AD_Menu { MenuID = 203, MenuParentID = 2, MenuText = "Loại công việc", URLAction = "/JobType", MenuIcon = "fa fa-info-circle", MenuSort = 5, MenuSeparator = null, CanDelete = null }
               
                , new AD_Menu { MenuID = 206, MenuParentID = 2, MenuText = "Phân loại người dùng", URLAction = "/UserType", MenuIcon = "fa fa-info-circle", MenuSort = 7, MenuSeparator = null, CanDelete = null }
                , new AD_Menu { MenuID = 207, MenuParentID = 2, MenuText = "Vị trí", URLAction = "/Location", MenuIcon = "fa fa-info-circle", MenuSort = 8, MenuSeparator = null, CanDelete = null }
                , new AD_Menu { MenuID = 208, MenuParentID = 2, MenuText = "Lĩnh vực", URLAction = "/Zone", MenuIcon = "fa fa-info-circle", MenuSort = 9, MenuSeparator = null, CanDelete = null }

                , new AD_Menu { MenuID = 209, MenuParentID = 2, MenuText = "Vị trí công việc", URLAction = "/JobPosition", MenuIcon = "fa fa-info-circle", MenuSort = 9, MenuSeparator = null, CanDelete = null }
                , new AD_Menu { MenuID = 210, MenuParentID = 2, MenuText = "Mức thu nhập", URLAction = "/SalaryLevel", MenuIcon = "fa fa-info-circle", MenuSort = 9, MenuSeparator = null, CanDelete = null }
                , new AD_Menu { MenuID = 211, MenuParentID = 2, MenuText = "Thời gian làm việc", URLAction = "/WorkingTime", MenuIcon = "fa fa-info-circle", MenuSort = 9, MenuSeparator = null, CanDelete = null }
                , new AD_Menu { MenuID = 212, MenuParentID = 2, MenuText = "Hợp đồng lao động", URLAction = "/ContractType", MenuIcon = "fa fa-info-circle", MenuSort = 9, MenuSeparator = null, CanDelete = null }
                , new AD_Menu { MenuID = 213, MenuParentID = 2, MenuText = "Tình trạng việc làm", URLAction = "/WorkingStatus", MenuIcon = "fa fa-info-circle", MenuSort = 9, MenuSeparator = null, CanDelete = null }
                , new AD_Menu { MenuID = 214, MenuParentID = 2, MenuText = "Câu hỏi", URLAction = "/Question", MenuIcon = "fa fa-info-circle", MenuSort = 6, MenuSeparator = null, CanDelete = null }

                , new AD_Menu { MenuID = 215, MenuParentID = 2, MenuText = "Nhóm câu trả lời", URLAction = "/Answer", MenuIcon = "fa fa-info-circle", MenuSort = 6, MenuSeparator = null, CanDelete = null }
                , new AD_Menu { MenuID = 216, MenuParentID = 2, MenuText = "Chi tiết câu trả lời", URLAction = "/AnswerDetail", MenuIcon = "fa fa-info-circle", MenuSort = 6, MenuSeparator = null, CanDelete = null }

                , new AD_Menu { MenuID = 230, MenuParentID = 2, MenuText = "Người dùng Bình chọn", URLAction = "/UserRatingCompany", MenuIcon = "fa fa-info-circle", MenuSort = 10, MenuSeparator = null, CanDelete = null }
                , new AD_Menu { MenuID = 231, MenuParentID = 2, MenuText = "Tuyển dụng", URLAction = "/CompanyZone", MenuIcon = "fa fa-info-circle", MenuSort = 11, MenuSeparator = null, CanDelete = null }
                , new AD_Menu { MenuID = 9, MenuParentID = null, MenuText = "QUẢN TRỊ", URLAction = "#", MenuIcon = "fa fa-cog", MenuSort = 31, MenuSeparator = null, CanDelete = null }
                , new AD_Menu { MenuID = 91, MenuParentID = 9, MenuText = "Quản lý Menu", URLAction = "/MenuManagement", MenuIcon = "fa fa-user", MenuSort = 35, MenuSeparator = null, CanDelete = null }
                , new AD_Menu { MenuID = 92, MenuParentID = 9, MenuText = "Người Dùng và Phân Quyền", URLAction = "/Permission", MenuIcon = "fa fa-user", MenuSort = 35, MenuSeparator = null, CanDelete = null }
                , new AD_Menu { MenuID = 100, MenuParentID = null, MenuText = "TRỢ GIÚP", URLAction = "#", MenuIcon = "fa fa-info-circle", MenuSort = 30, MenuSeparator = null, CanDelete = null }
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

           
            context.JobType.AddOrUpdate(a => a.JobTypeID,
                new JobType { JobTypeID = 1, JobTypeName = "Toàn thời gian" },
                new JobType { JobTypeID = 2, JobTypeName = "Bán thời gian" },
                new JobType { JobTypeID = 3, JobTypeName = "Hợp đồng" },
                new JobType { JobTypeID = 4, JobTypeName = "Thực tập" },
                new JobType { JobTypeID = 5, JobTypeName = "Thử việc" },
                new JobType { JobTypeID = 6, JobTypeName = "Thời vụ" },
                new JobType { JobTypeID = 7, JobTypeName = "Khác" }
            );


            #region Zone
            context.Zone.AddOrUpdate(a => a.ZoneID,
                     new Zone { ZoneID = 1,  ZoneName = "Kế toán-Kiểm toán" },
                     new Zone { ZoneID = 2,  ZoneName = "Bán hàng" },
                     new Zone { ZoneID = 3,  ZoneName = "Marketing-PR" },
                     new Zone { ZoneID = 4,  ZoneName = "Tư vấn" },
                     new Zone { ZoneID = 5,  ZoneName = "KD bất động sản" },
                     new Zone { ZoneID = 6,  ZoneName = "Xây dựng" },
                     new Zone { ZoneID = 7,  ZoneName = "IT phần mềm" },
                     new Zone { ZoneID = 8, ZoneName = "Điện-Điện tử" },
                     new Zone { ZoneID = 9, ZoneName = "Y tế-Dược" },
                     new Zone { ZoneID = 10, ZoneName = "Cơ khí-Chế tạo" },
                     new Zone { ZoneID = 11, ZoneName = "Kỹ thuật" },
                     new Zone { ZoneID = 12, ZoneName = "Kiến trúc-TK nội thất" },
                     new Zone { ZoneID = 13, ZoneName = "Nhân sự" },
                     new Zone { ZoneID = 14, ZoneName = "Biên-Phiên dịch" },
                     new Zone { ZoneID = 15, ZoneName = "Giáo dục-Đào tạo" },
                     new Zone { ZoneID = 16, ZoneName = "Quản trị kinh doanh" },
                     new Zone { ZoneID = 17, ZoneName = "Xuất, nhập khẩu" },
                     new Zone { ZoneID = 18, ZoneName = "IT phần cứng/mạng" },
                     new Zone { ZoneID = 19, ZoneName = "Thiết kế-Mỹ thuật" },
                     new Zone { ZoneID = 20, ZoneName = "Khách sạn-Nhà hàng" },
                     new Zone { ZoneID = 21, ZoneName = "Thư ký-Trợ lý" },
                     new Zone { ZoneID = 22, ZoneName = "Dịch vụ" },
                     new Zone { ZoneID = 23, ZoneName = "Điện tử viễn thông" },
                     new Zone { ZoneID = 24, ZoneName = "Thiết kế đồ hoạ web" },
                     new Zone { ZoneID = 25, ZoneName = "Dệt may - Da giày" },
                     new Zone { ZoneID = 26, ZoneName = "Tiếp thị-Quảng cáo" },
                     new Zone { ZoneID = 27, ZoneName = "Thương mại điện tử" },
                     new Zone { ZoneID = 28, ZoneName = "Vật tư-Thiết bị" },
                     new Zone { ZoneID = 29, ZoneName = "Kỹ thuật ứng dụng" },
                     new Zone { ZoneID = 30, ZoneName = "Báo chí-Truyền hình" },
                     new Zone { ZoneID = 31, ZoneName = "Ngành nghề khác" },
                     new Zone { ZoneID = 32, ZoneName = "Ngân hàng" },
                     new Zone { ZoneID = 33, ZoneName = "Thực phẩm-Đồ uống" },
                     new Zone { ZoneID = 34, ZoneName = "Ô tô - Xe máy" },
                     new Zone { ZoneID = 35, ZoneName = "Du lịch" },
                     new Zone { ZoneID = 36, ZoneName = "Thời trang" },
                     new Zone { ZoneID = 37, ZoneName = "Vận tải" },
                     new Zone { ZoneID = 38, ZoneName = "Bảo hiểm" },
                     new Zone { ZoneID = 39, ZoneName = "Công nghiệp" },
                     new Zone { ZoneID = 40, ZoneName = "Hoá học-Sinh học" },
                     new Zone { ZoneID = 41, ZoneName = "Pháp lý" },
                     new Zone { ZoneID = 42, ZoneName = "In ấn-Xuất bản" },
                     new Zone { ZoneID = 43, ZoneName = "Mỹ phẩm-Trang sức" },
                     new Zone { ZoneID = 44, ZoneName = "Nông-Lâm-Ngư nghiệp" },
                     new Zone { ZoneID = 45, ZoneName = "Quan hệ đối ngoại" },
                     new Zone { ZoneID = 46, ZoneName = "Tổ chức sự kiện-Quà tặng" },
                     new Zone { ZoneID = 47, ZoneName = "Hoạch định-Dự án" },
                     new Zone { ZoneID = 48, ZoneName = "Hàng gia dụng" },
                     new Zone { ZoneID = 49, ZoneName = "Dầu khí-Hóa chất" },
                     new Zone { ZoneID = 50, ZoneName = "Công nghệ cao" },
                     new Zone { ZoneID = 51, ZoneName = "Nghệ thuật - Điện ảnh" },
                     new Zone { ZoneID = 52, ZoneName = "Bưu chính" },
                     new Zone { ZoneID = 53, ZoneName = "Bảo vệ" },
                     new Zone { ZoneID = 54, ZoneName = "Game" },
                     new Zone { ZoneID = 55, ZoneName = "Chứng khoán- Vàng" },
                     new Zone { ZoneID = 56, ZoneName = "Hàng không" },
                     new Zone { ZoneID = 57, ZoneName = "Đầu tư" },
                     new Zone { ZoneID = 58, ZoneName = "Thủ công mỹ nghệ" },
                     new Zone { ZoneID = 59, ZoneName = "Hàng hải" }
              );

            #endregion

            context.Company.AddOrUpdate(a => a.CompanyID,
                  new Company { CompanyID = 1, CompanyName = "CodeLove", LocationID = 1 },
                  new Company { CompanyID = 2, CompanyName = "RunSystem", LocationID = 1 }
              );

            context.Job.AddOrUpdate(a => a.JobID,
                new Job { JobID = 1, JobName = "Lập trình viên C#", CompanyID = 1, JobTypeID =1 },
                new Job { JobID = 2, JobName = "Web Designer", CompanyID = 1, JobTypeID = 1 },
                new Job { JobID = 3, JobName = "Phiên dịch", CompanyID = 2, JobTypeID = 1 }
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

            context.UserRatingCompany.AddOrUpdate(a => new { a.CompanyID, a.QuestionID, a.AnswerID, a.UserID },
                new UserRatingCompany { CompanyID = 1, QuestionID = 1, AnswerID = 1, UserID = "Admin", Score = 2.0m }
                );

            context.CompanyZone.AddOrUpdate(
                 a => new { a.CompanyID, a.ZoneID },
                new CompanyZone { CompanyID = 1, ZoneID = 1 },
                new CompanyZone { CompanyID = 1, ZoneID = 2 },
                 new CompanyZone { CompanyID = 2, ZoneID = 1 },
                new CompanyZone { CompanyID = 2, ZoneID = 2 }
            );


            context.JobPosition.AddOrUpdate(a => a.JobPositionID,
                new JobPosition { JobPositionID = 1, JobPositionName = "Trưởng phòng" },
                new JobPosition { JobPositionID = 2, JobPositionName = "Nhân viên" },
                new JobPosition { JobPositionID = 3, JobPositionName = "Công nhân" },
                new JobPosition { JobPositionID = 4, JobPositionName = "Cán bộ" },
                new JobPosition { JobPositionID = 5, JobPositionName = "Giám sát" },
                new JobPosition { JobPositionID = 6, JobPositionName = "Quản lý cấp cao" }
            );


            context.WorkingStatus.AddOrUpdate(a => a.WorkingStatusID,
                new WorkingStatus { WorkingStatusID = 1, WorkingStatusName = "Đang đàm phán hợp đồng" },
                new WorkingStatus { WorkingStatusID = 2, WorkingStatusName = "Đang thử việc" },
                new WorkingStatus { WorkingStatusID = 3, WorkingStatusName = "Đang thực hiện hợp đồng" },
                new WorkingStatus { WorkingStatusID = 4, WorkingStatusName = "Đang đàm phán lại hợp đồng" },
                new WorkingStatus { WorkingStatusID = 5, WorkingStatusName = "Chấm dứt hợp đồng" },
                new WorkingStatus { WorkingStatusID = 6, WorkingStatusName = "Khác" }
            );



            context.WorkingTime.AddOrUpdate(a => a.WorkingTimeID,
                new WorkingTime { WorkingTimeID = 1, WorkingTimeName = "Dưới 3 tháng" },
                new WorkingTime { WorkingTimeID = 2, WorkingTimeName = "Trên 3 tháng - dưới 6 tháng" },
                new WorkingTime { WorkingTimeID = 3, WorkingTimeName = "Trên 6 tháng - dưới 12 tháng" },
                new WorkingTime { WorkingTimeID = 4, WorkingTimeName = "Trên 12 tháng - dưới 24 tháng" },
                new WorkingTime { WorkingTimeID = 5, WorkingTimeName = "Trên 24 tháng - dưới 36 tháng" },
                new WorkingTime { WorkingTimeID = 6, WorkingTimeName = "Trên 36 tháng VNĐ" }
            );


            context.ContractType.AddOrUpdate(a => a.ContractTypeID,
                new ContractType { ContractTypeID = 1, ContractTypeName = "Không có hợp đồng" },
                new ContractType { ContractTypeID = 2, ContractTypeName = "Hợp đồng thử việc" },
                new ContractType { ContractTypeID = 3, ContractTypeName = "Hợp đồng thời vụ" },
                new ContractType { ContractTypeID = 4, ContractTypeName = "Hợp đồng xác định thời hạn" },
                new ContractType { ContractTypeID = 5, ContractTypeName = "Hợp đồng không xác định thời hạn" },
                new ContractType { ContractTypeID = 6, ContractTypeName = "Khác" }
            );



            context.SalaryLevel.AddOrUpdate(a => a.SalaryLevelID,
                new SalaryLevel { SalaryLevelID = 1, SalaryLevelName = "Dưới 5 triệu VNĐ" },
                new SalaryLevel { SalaryLevelID = 2, SalaryLevelName = "Trên 5 triệu - dưới 10 triệu" },
                new SalaryLevel { SalaryLevelID = 3, SalaryLevelName = "Trên 10 triệu - dưới 20 triệu" },
                new SalaryLevel { SalaryLevelID = 4, SalaryLevelName = "Trên 20 triệu - dưới 30 triệu" },
                new SalaryLevel { SalaryLevelID = 5, SalaryLevelName = "Trên 30 triệu - dưới 40 triệu" },
                new SalaryLevel { SalaryLevelID = 6, SalaryLevelName = "Trên 40 triệu - dưới 50 triệu" },
                new SalaryLevel { SalaryLevelID = 7, SalaryLevelName = "Trên 50 triệu VNĐ" }
            );


            context.Question.AddOrUpdate(a => a.QuestionID,
                new Question { QuestionID = 1, QuestionName = "Trong số 05 khía cạnh dưới đây, ở khía cạnh nào bạn thấy QUYỀN của mình bị vi phạm nhiều nhất?" },
                new Question { QuestionID = 2, QuestionName = "Trong số 05 khía cạnh dưới đây, ở khía cạnh nào bạn thấy khó ĐÀM PHÁN với chủ sử  dụng lao động nhất?" },
                new Question { QuestionID = 3, QuestionName = "Trong số 05 khía cạnh dưới đây, ở khía cạnh nào bạn thấy thiếu MINH BẠCH nhất?" }
            );

            context.Answer.AddOrUpdate(a => a.AnswerID,
                new Answer { AnswerID = 1, AnswerName = "Công việc, sự nghiệp" },
                new Answer { AnswerID = 2, AnswerName = "Lương & phúc lợi" },
                new Answer { AnswerID = 3, AnswerName = "An toàn và sức khỏe" },
                new Answer { AnswerID = 4, AnswerName = "Tổ chức & quản lý" },
                new Answer { AnswerID = 5, AnswerName = "Khía cạnh khác" }
            );



            context.AnswerDetail.AddOrUpdate(a => a.AnswerDetailID,
                new AnswerDetail { AnswerDetailID = 1, AnswerID = 1, AnswerDetailName = "Thôi việc & bị thôi việc" },
                new AnswerDetail { AnswerDetailID = 2, AnswerID = 1, AnswerDetailName = "Chuyển việc & tạm dừng làm việc" },
                new AnswerDetail { AnswerDetailID = 3, AnswerID = 1, AnswerDetailName = "Khen thưởng & Kỷ luật" },
                new AnswerDetail { AnswerDetailID = 4, AnswerID = 1, AnswerDetailName = "Cơ hội và phát triển" },
                new AnswerDetail { AnswerDetailID = 5, AnswerID = 1, AnswerDetailName = "Vấn đề khác" },
                new AnswerDetail { AnswerDetailID = 6, AnswerID = 2, AnswerDetailName = "Trả lương tối thiểu" },
                new AnswerDetail { AnswerDetailID = 7, AnswerID = 2, AnswerDetailName = "Trả lương cạnh tranh" },
                new AnswerDetail { AnswerDetailID = 8, AnswerID = 2, AnswerDetailName = "Trả thưởng & phụ cấp" },
                new AnswerDetail { AnswerDetailID = 9, AnswerID = 2, AnswerDetailName = "Trả lương làm thêm giờ" },
                new AnswerDetail { AnswerDetailID = 10, AnswerID = 2, AnswerDetailName = "Bảo hiểm & phúc lợi KHÁC" },
                new AnswerDetail { AnswerDetailID = 11, AnswerID = 3, AnswerDetailName = "Tai nạn nghề nghiệp" },
                new AnswerDetail { AnswerDetailID = 12, AnswerID = 3, AnswerDetailName = "Vệ sinh lao động" },
                new AnswerDetail { AnswerDetailID = 13, AnswerID = 3, AnswerDetailName = "Sức khỏe nghề nghiệp" },
                new AnswerDetail { AnswerDetailID = 14, AnswerID = 3, AnswerDetailName = "Môi trường lành mạnh" },
                new AnswerDetail { AnswerDetailID = 15, AnswerID = 3, AnswerDetailName = "Vấn đề khác" },
                new AnswerDetail { AnswerDetailID = 16, AnswerID = 4, AnswerDetailName = "Chính sách và quản trị nhân sự" },
                new AnswerDetail { AnswerDetailID = 17, AnswerID = 4, AnswerDetailName = "Năng lực lãnh đạo" },
                new AnswerDetail { AnswerDetailID = 18, AnswerID = 4, AnswerDetailName = "Quy trình tổ chức quản lý" },
                new AnswerDetail { AnswerDetailID = 19, AnswerID = 4, AnswerDetailName = "Phương pháp tổ chức quản lý" },
                new AnswerDetail { AnswerDetailID = 20, AnswerID = 4, AnswerDetailName = "Báo cáo & thực thi" },
                new AnswerDetail { AnswerDetailID = 21, AnswerID = 5, AnswerDetailName = "Phân biệt đối xử" },
                new AnswerDetail { AnswerDetailID = 22, AnswerID = 5, AnswerDetailName = "Lao động cưỡng bức" },
                new AnswerDetail { AnswerDetailID = 23, AnswerID = 5, AnswerDetailName = "Lao động trẻ em" },
                new AnswerDetail { AnswerDetailID = 24, AnswerID = 5, AnswerDetailName = "Quấy rối tình dục" },
                new AnswerDetail { AnswerDetailID = 25, AnswerID = 5, AnswerDetailName = "Tự do phản biện" }
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
            var user = new ApplicationUser { UserName = "Admin", Email = "admin@somedomain.com",  FullName = "System Administrator", LastModified = DateTime.Now, Inactive = false, EmailConfirmed = true };

            ApplicationUserManager UserManager = new ApplicationUserManager(new ApplicationUserStore(context));
            var result = UserManager.Create(user, "Admin");

            if (result.Succeeded)
            {
                //Add User to Admin Role...
                UserManager.AddToRole(user.Id, c_SysAdmin);
            }


            //Create Default User...
            user = new ApplicationUser { UserName = "User", Email = "defaultuser@somedomain.com",  FullName = "Default User", LastModified = DateTime.Now, Inactive = false, EmailConfirmed = true };
            result = UserManager.Create(user, "User");

            if (result.Succeeded)
            {
                //Add User to Admin Role...
                UserManager.AddToRole(user.Id, c_DefaultUser);
            }

            //Create User with NO Roles...
            user = new ApplicationUser { UserName = "Guest", Email = "guest@somedomain.com", FullName = "Guest User", LastModified = DateTime.Now, Inactive = false, EmailConfirmed = true };
            result = UserManager.Create(user, "Guest");


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