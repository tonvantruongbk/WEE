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
  new Company { CompanyID = 1, CompanyName = "Công ty cổ phần đầu tư TH Vina", Address = "Số 66, phố Dịch Vọng, phường Dịch Vọng, quận Cầu Giấy, thành phố Hà Nội, Việt Nam", PhoneNumber = null, Description = null, Website = "http://thvina.net/", Logo = "/Content/UPLOAD/thvina.jpg", LocationID = 24, AverageScore = 2.2m, TotalUserRate = 0, TotalJobActive = null, }
   , new Company { CompanyID = 2, CompanyName = "Công ty TNHH XNK Cầu Nối Việt", Address = "Tầng 6, tòa nhà 6a, Ngõ 639, Đường Hoàng Hoa Thám, Ba Đình, Hà Nội", PhoneNumber = null, Description = "Công ty TNHH XNK Cầu Nối Việt là nhà nhập khẩu và phân phối chuyên nghiệp các thương hiệu hàng đầu thế giới phục vụ cho nhu cầu của bà mẹ và trẻ em tại Việt Nam. Với tốc độ tăng trưởng nhanh chóng, hiện nay chúng tôi phục vụ khách hàng thông qua 10 showroom tại các trung tâm thương mại cao cấp nhất ở Hà Nội và TP Hồ Chí Minh, cùng với hàng trăm đại lý là các cửa hàng mẹ bé trên toàn quốc.  Trải qua quãng thời gian từ khi khởi nghiệp tới nay, Cầu Nối Việt đã khẳng định được uy tín của mình trước khách hàng và đối tác bằng đội ngũ nhân viên trẻ, có trình độ, năng động sáng tạo và đầy nhiệt huyết. Công ty luôn quan tâm xây dựng văn hóa doanh nghiệp hiện đại, nỗ lực tạo dựng một môi trường làm việc hòa đồng, gắn kết, phát triển trên cơ sở hỗ trợ lẫn nhau, giúp đội ngũ cán bộ nhân viên bộc lộ và phát huy đối đa thế mạnh của cá nhân mình.  Cầu Nối Việt luôn coi trọng việc tuyển dụng và đào tạo các nhân viên có năng lực. Khi gia nhập đại gia đình Cầu Nối Việt các ứng viên sẽ được tham gia các khóa huấn luyện, được tư vẫn hỗ trợ về các kĩ năng nghiệp vụ chuyên môn để có thể phát triển sự nghiệp thành công và gắn bó lâu dài với công ty.", Website = "https://www.mamanbebe.com.vn", Logo = "/Content/UPLOAD/logomamanbebe.jpg", LocationID = 24, AverageScore = null, TotalUserRate = 12, TotalJobActive = null, }
   , new Company { CompanyID = 3, CompanyName = "Tập Đoàn Hoa Sao", Address = "Tầng 3- Tòa nhà Trung Yên 1 - Số 1 Vũ Phạm Hàm - Yên Hòa - Cầu Giấy - Hà Nội", PhoneNumber = null, Description = "Tổng quan về công ty  Được thành lập vào năm 2006, sau 11 năm thành lập, Hoa Sao đã trở thành doanh nghiệp hàng đầu tại Việt Nam trong lĩnh vực Contact Center và BPO với quy mô 4000 nhân viên làm việc trên 10 chi nhánh, phủ sóng từ: Hà Nội, Thái Nguyên, Điện Biên Vĩnh Phúc, Hải Phòng cho đến Đà Nẵng, Buôn Mê Thuật và Thành phố Hồ Chí Minh.  Vào cuối năm 2016, Hoa Sao bước sang một trang mới khi hợp tác cùng Tập đoàn số 1 về Contact Center tại Nhật Bản – BellSystem 24 tạo ra liên minh cung cấp dịch vụ Chăm sóc khách hàng mới mang tầm vóc quốc tế với tên gọi BellSystem24-Hoa Sao.  Với sự phát triển từ quy mô đến bề sâu chất lượng, Hoa Sao đã nhiều lần được ghi nhận là một đơn vị cung cấp dịch vụ uy tín và đón nhận nhiều giải thưởng lớn như: Top 40 Doanh nghiệp BPO, IT & KPO hàng đầu Việt Nam, xếp hạng bởi VINASA – Hiệp hội Phần mềm và Dịch vụ CNTT Việt Nam; giải thưởng Thương hiệu mạnh năm 2014 do Thời báo Kinh tế Việt Nam bình chọn cùng nhiều giải thưởng uy tín khác.  Hoa Sao tự hào khi trở thành đơn vị cung cấp dịch vụ cho hàng trăm thương hiệu top đầu tại Việt Nam như: Viettel, VTV, Vietjet Air, VinGroup, Vietin Bank...và là đối tác lâu dài của những tập đoàn hàng đầu quốc tế như BMW, Uber...  Môi trường làm việc và văn hóa doanh nghiệp  Khi gia nhập vào ngôi nhà chung Hoa Sao, bạn không chỉ được làm việc trong môi trường hội nhập quốc tế, phong cách làm việc chuyên nghiệp, bài bản mà còn có cơ hội nâng cao kiến thức, kỹ năng, các mối quan hệ xã hội để phát triển toàn diện.  Với chiến lược, lấy con người làm yếu tố chủ lực để phát triển, Hoa Sao liên tục tổ chức các hoạt động đào tạo nội bộ với các chuyên gia trong và ngoài ngoài để đội ngũ nhân viên được nâng cao kỹ năng, kiến thức.   Không dừng lại đó, người Hoa Sao còn được tham gia các hoạt động văn hóa, team building để tăng động lực đồng thời gắn kết thành viên trong toàn Công ty như: cuộc thi “I Love My Voice”, Hoa Sao Day, tổ chức sinh nhật, hoạt động kỷ niệm các ngày lễ...   Mỗi cá nhân tại Hoa Sao dù ở cấp bậc, vị trí nào cũng luôn được ghi nhận những đóng góp, nỗ lực của mình bằng các hoạt động vinh danh thiết thực như: Person of Month, Person of Year...  Khi dịch vụ CSKH cùng các Contact Center trở thành xu hướng và yếu tố cạnh tranh thúc đẩy sự phát triển cho các thương hiệu thì làm việc tại Bellsystem24-HoaSao – Tập đoàn BPO quốc tế hàng đầu tại Việt Nam sẽ mở ra cho bạn những cơ hội tuyệt vời để phát triển và thành công trong sự nghiệp của mình. Trở thành người Hoa Sao bạn còn có cợ hội để được thể hiện và chứng tỏ khả năng, được ghi nhận và thăng tiến, được quan tâm và chia sẻ.", Website = "http://hoasao.vn", Logo = "/Content/UPLOAD/hoasao.png", LocationID = 24, AverageScore = 3.2m, TotalUserRate = 0, TotalJobActive = null, }
   , new Company { CompanyID = 4, CompanyName = "Công ty TNHH Minh Phúc (MP Telecom)", Address = "Số 36-38A Trần Văn Dư, P.13, Quận Tân Bình, Tp. HCM", PhoneNumber = null, Description = "Công ty TNHH Minh Phúc (MP Telecom) là công ty hàng đầu tại Việt Nam chuyên cung cấp các dịch vụ và giải pháp Contact Center, Đào tạo và Cung ứng nhân lực, VAS và BPO (Business Process Outsourcing) tại Việt Nam. Được thành lập từ năm 2002, MP Telecom đã không ngừng lớn mạnh cả về quy mô tổ chức lẫn chất lượng dịch vụ. Hiện nay chúng tôi có hơn 2.000 nhân viên trên 3 miền Bắc, Trung, Nam. MP Telecom là công ty đầu tiên trong lĩnh vực BPO tại Việt Nam được nhận chứng chỉ quản lý chất lượng ISO 9001:2008 và Hệ thống quản lý an ninh thông tin ISO/IEC 27001:2005. Kinh nghiệm dạn dày của chúng tôi được thể hiện qua rất nhiều dự án đã và đang hợp tác với các Khách hàng lớn như: Mobifone, VinaPhone, Viettel, BIDV, ANZ, VPBank, Ocean Bank, Bảo Việt, VTV Cab, Toto, Trần Anh, Mai Linh, Truyền hình An Viên, Acecook, EVN…   ", Website = "www.mptelecom.com.vn", Logo = "/Content/UPLOAD/minhphuc.png", LocationID = 31, AverageScore =2.00m, TotalUserRate = 0, TotalJobActive = null, }
   , new Company { CompanyID = 5, CompanyName = "Công ty cổ phần Funtap", Address = "P803, tầng 8, tòa nhà Toyota, 315 Trường Chinh, Thanh Xuân, Hà Nộii", PhoneNumber = null, Description = "Công ty Cổ phần Funtap (Website: funtap.vn) Được thành lập đầu năm 2013 bởi đội ngũ kỹ sư giàu nhiệt huyết và kinh nghiệm trong lĩnh vực dịch vụ Internet và giải trí trên nền tảng di động. FunTap là ngôi nhà tập hợp người trẻ yêu công nghệ, cá tính với khát khao mang đến cho cộng đồng người tiêu dùng Việt Nam các sản phẩm giải trí online đột phá và sáng tạo. Tại FunTap, chúng tôi tôn trọng sự khác biệt và sự bình đẳng của từng cá nhân. Đó là nền tảng cơ bản giúp chúng tôi luôn mang đến cho khách hàng sự trải nghiệm dịch vụ hoàn hảo. Làm việc tại Funtap, bạn có cơ hội được tiếp xúc với đội ngũ chuyên gia công nghệ hàng đầu, được tham gia vào các dự án với khách hàng quốc tế và trong nước. Môi trường làm việc trẻ và năng động, chế độ đãi ngộ xứng đáng và có nhiều cơ hội phát triển kỹ năng nghề nghiệp.", Website = "http://corp.funtap.vn/", Logo = "/Content/UPLOAD/funtap.png", LocationID = 24, AverageScore = 3.00m, TotalUserRate = 14, TotalJobActive = null, }
   , new Company { CompanyID = 6, CompanyName = "Công ty TNHH Đầu tư và Kỹ thuật Hải An", Address = "257 Liên Chiểu - Đà Nẵng", PhoneNumber = null, Description = "NHÀ THẦU CHUYÊN NGHIỆP VỀ HỆ THỐNG ĐIỀU HÒA chuyên thiết kế, cung cấp, tư vấn lắp đặt hệ thống điều hòa", Website = "www.mallcenter.vn", Logo = "/Content/UPLOAD/1476158725_hai_an.PNG", LocationID = 15, AverageScore = 3.00m, TotalUserRate = 5, TotalJobActive = null, }
   , new Company { CompanyID = 7, CompanyName = "Công ty cổ phần kỹ nghệ kingtech", Address = "263 đường La Dương, Dương Nội, Hà Đông, Hà Nội", PhoneNumber = null, Description = null, Website = "http://kingtech.vn/", Logo = "/Content/UPLOAD/amc.png", LocationID = 24, AverageScore = 4.50m, TotalUserRate = 2, TotalJobActive = null, }
   , new Company { CompanyID = 8, CompanyName = "Công ty cổ phần Nhựa Á Đông", Address = "CN: 38A, Le Van Huan street, 13 ward, Tan Binh, HCM, Viet Nam. , Trụ sở : 27 Lại Yên, Hoài Đức, Hà Nội", PhoneNumber = null, Description = "Công ty Cổ phần Nhựa Á Đông là một trong những doanh nghiệp sản xuất, kinh doanh và xuất nhập khẩu hàng đầu ở Việt Nam hiện nay. Công ty chuyên sản xuất hạt phụ gia dùng trong ngành nhựa gồm Hợp chất CaCO3 (CALMAST), Hạt nhựa màu (COLMAST) và Hạt phụ gia chức năng (ADDMAST). Với định hướng phát triển dài hạn chúng tôi không ngừng cải tiến chất lượng, mở rộng sản xuất, để sản phẩm đáp ứng yêu cầu ngày càng cao của khách hàng về chất lượng, số lượng và giá thành cạnh tranh, Nhựa Á Đông đã có 2 nhà máy với các dây chuyền sản xuất hiện đại đạt công suất khoảng 60 ngàn tấn/năm kể từ năm 2012. Bên cạnh đó các dự án tăng trưởng nhà máy và đầu tư công nghệ hiện đại cũng liên tục được thực hiện nên công suất nhà máy cũng được tăng lên qua từng thời kỳ. ", Website = "adcplastic.com", Logo = "/Content/UPLOAD/logo-adc.jpg", LocationID = 31, AverageScore = 4.00m, TotalUserRate = 3, TotalJobActive = null, }
  );

            context.Job.AddOrUpdate(a => a.JobID,
  new Job { JobID = 1, JobName = " Trưởng Phòng Nhân Sự", DateCreate = null, DateEnd = null, CompanyID = 1, JobTypeID = 1, }
   , new Job { JobID = 2, JobName = "Nhân Viên Viết Bài Quảng Cáo", DateCreate = null, DateEnd = null, CompanyID = 1, JobTypeID = 1, }
   , new Job { JobID = 3, JobName = "Nhân viên bán hàng tại Aeon Mall Bình Tân", DateCreate = null, DateEnd = null, CompanyID = 2, JobTypeID = 2, }
   , new Job { JobID = 4, JobName = "Digital Marketing Senior Staff", DateCreate = DateTime.Parse("10/03/2017"), DateEnd = DateTime.Parse("10/12/2017"), CompanyID = 2, JobTypeID = 1, }
   , new Job { JobID = 5, JobName = "Nhân Viên Tư Vấn Dịch Vụ Quảng Cáo", DateCreate = DateTime.Parse("10/04/2017"), DateEnd = DateTime.Parse("10/12/2017"), CompanyID = 1, JobTypeID = 2, }
   , new Job { JobID = 6, JobName = "Nhân Viên Khai Thác Thị Trường", DateCreate = null, DateEnd = null, CompanyID = 2, JobTypeID = 3, }
   , new Job { JobID = 7, JobName = "Nhân Viên Khảo Sát Sản Phẩm - Giờ Hành Chính", DateCreate = null, DateEnd = null, CompanyID = 4, JobTypeID = 1, }
   , new Job { JobID = 8, JobName = "Nhân Viên Chăm Sóc Khách Hàng Mạng Viettel Tại Kđt Resco Phạm Văn Đồng", DateCreate = null, DateEnd = null, CompanyID = 4, JobTypeID = 2, }
   , new Job { JobID = 9, JobName = "Telesales Mỹ Phẩm (Ba Đình, Hà Nội)", DateCreate = null, DateEnd = null, CompanyID = 3, JobTypeID = 3, }
   , new Job { JobID = 10, JobName = "SV trự tổng đài yêu cầu NGHE - HIỂU tiếng Anh", DateCreate = null, DateEnd = null, CompanyID = 3, JobTypeID = 5, }
   , new Job { JobID = 11, JobName = "Nhân Viên Đặc Lịch Hẹn Khách Hàng - Làm Việc Quận Tân Bình", DateCreate = null, DateEnd = null, CompanyID = 3, JobTypeID = 4, }
   , new Job { JobID = 12, JobName = "Nhân Viên Thiết Kế Sản Phẩm Internet", DateCreate = DateTime.Parse("10/04/2017"), DateEnd = DateTime.Parse("10/12/2017"), CompanyID = 5, JobTypeID = 1, }
   , new Job { JobID = 13, JobName = "Vận Hành Game (Quản Lý Sản Phẩm)", DateCreate = DateTime.Parse("10/01/2017"), DateEnd = DateTime.Parse("10/11/2017"), CompanyID = 5, JobTypeID = 2, }
   , new Job { JobID = 14, JobName = "Nhân Viên Tìm Và Đánh Giá Game", DateCreate = null, DateEnd = null, CompanyID = 5, JobTypeID = 4, }
   , new Job { JobID = 15, JobName = "Nhân viên lắp đặt điều hòa - Mức lương hấp dẫn", DateCreate = DateTime.Parse("10/01/2017"), DateEnd = DateTime.Parse("12/12/2017"), CompanyID = 6, JobTypeID = 1, }
   , new Job { JobID = 16, JobName = "Chỉ Huy Trưởng Công Trường Lương 15-20 Triệu / tháng", DateCreate = null, DateEnd = null, CompanyID = 6, JobTypeID = 1, }
   , new Job { JobID = 17, JobName = "Nhân Viên Kĩ Thuật Điện Thoại Di Động", DateCreate = null, DateEnd = null, CompanyID = 6, JobTypeID = 3, }
   , new Job { JobID = 18, JobName = "Kỹ Sư Cơ Điện Khu Vực Miền Bắc", DateCreate = null, DateEnd = null, CompanyID = 6, JobTypeID = 1, }
   , new Job { JobID = 19, JobName = "Chuyên viên xuất nhập khẩu", DateCreate = null, DateEnd = null, CompanyID = 7, JobTypeID = 1, }
   , new Job { JobID = 20, JobName = "Nhân Viên Kỹ Thuật- Mức Lương: 7-10 Triệu / Tháng", DateCreate = null, DateEnd = null, CompanyID = 7, JobTypeID = 3, }
   , new Job { JobID = 21, JobName = "Thợ hàn Tig, Mig (lương 7-10 triệu)", DateCreate = null, DateEnd = null, CompanyID = 7, JobTypeID = 1, }
   , new Job { JobID = 22, JobName = "Kế Toán Tổng Hợp", DateCreate = null, DateEnd = null, CompanyID = 8, JobTypeID = 1, }
   , new Job { JobID = 23, JobName = "Nhân Viên Bảo Vệ .", DateCreate = DateTime.Parse("10/01/2017"), DateEnd = DateTime.Parse("12/12/2017"), CompanyID = 8, JobTypeID = 5, }
   , new Job { JobID = 24, JobName = "Công Nhân Sản Xuất Tại Nhà Máy Hoài Đức", DateCreate = DateTime.Parse("08/03/2017"), DateEnd = DateTime.Parse("12/12/2017"), CompanyID = 8, JobTypeID = 3, }
   , new Job { JobID = 25, JobName = "Nhân Viên Kinh Doanh (Lương 10-15 Triệu)", DateCreate = DateTime.Parse("07/12/2017"), DateEnd = null, CompanyID = 8, JobTypeID = 2, }
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
            var result = UserManager.Create(user, "Admin123");

            if (result.Succeeded)
            {
                //Add User to Admin Role...
                UserManager.AddToRole(user.Id, c_SysAdmin);
            }


            //Create Default User...
            user = new ApplicationUser { UserName = "User", Email = "defaultuser@somedomain.com",  FullName = "Default User", LastModified = DateTime.Now, Inactive = false, EmailConfirmed = true };
            result = UserManager.Create(user, "User123");

            if (result.Succeeded)
            {
                //Add User to Admin Role...
                UserManager.AddToRole(user.Id, c_DefaultUser);
            }

            //Create User with NO Roles...
            user = new ApplicationUser { UserName = "Guest", Email = "guest@somedomain.com", FullName = "Guest User", LastModified = DateTime.Now, Inactive = false, EmailConfirmed = true };
            result = UserManager.Create(user, "Guest123");


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