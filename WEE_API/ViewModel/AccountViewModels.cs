using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WEE_API.Common;

namespace WEE_API.ViewModel
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "UserName")]      
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
 
    public class RegisterViewModel
    {
   
        [Required]
        [StringLength(15, ErrorMessage = CommonValidationString.StringLengh, MinimumLength = 6)]
        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }

        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = CommonValidationString.StringLengh, MinimumLength = 2)]
        [Display(Name = "Họ và tên")]
        public string FullName { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = CommonValidationString.StringLengh, MinimumLength = 2)]
        [Display(Name = "First name")]
        public string Firstname { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = CommonValidationString.StringLengh, MinimumLength = 2)]
        [Display(Name = "Last name")]
        public string Lastname { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = CommonValidationString.StringLengh, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Nhập lại mật khẩu")]
        [Compare("Password", ErrorMessage = CommonValidationString.Password)]
        public string ConfirmPassword { get; set; }
                
        [StringLength(30, ErrorMessage = CommonValidationString.StringLengh, MinimumLength = 11)]
        [Display(Name = "Điện thoại")]
        public string Mobile { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = CommonValidationString.StringLengh, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = CommonValidationString.Password)]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }


    //********** RBAC View Models **************
    public class RoleViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = CommonValidationString.StringLengh, MinimumLength = 2)]
        [Display(Name = "Role Name")]
        public string Name { get; set; }


        [Required]
        [StringLength(30, ErrorMessage = CommonValidationString.StringLengh, MinimumLength = 2)]
        [Display(Name = "Role Description")]
        public string RoleDescription { get; set; }

        [Required]       
        [Display(Name = "Is System Administrator")]
        public bool IsSysAdmin { get; set; }
    }

    public class UserViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = CommonValidationString.StringLengh, MinimumLength = 6)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = CommonValidationString.StringLengh, MinimumLength = 2)]
        [Display(Name = "First name")]
        public string Firstname { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = CommonValidationString.StringLengh, MinimumLength = 2)]
        [Display(Name = "Last name")]
        public string Lastname { get; set; }

       
    }

}
