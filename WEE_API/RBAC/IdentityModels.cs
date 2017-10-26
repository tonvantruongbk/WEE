using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WEE_API.Common;
using WEE_API.Models;

namespace WEE_API.RBAC
{
    public class ApplicationUserLogin : IdentityUserLogin<int> { }
    public class ApplicationUserClaim : IdentityUserClaim<int> { }

    public class ApplicationUserRole : IdentityUserRole<int>
    {
        public ApplicationRole Role { get; set; }

        public bool IsPermissionInRole(string _permission)
        {
            bool _retVal = false;
            try
            {          
                _retVal = this.Role.IsPermissionInRole(_permission);             
            }
            catch (Exception)
            {
            }
            return _retVal;
        }

        public bool IsSysAdmin { get { return this.Role.IsSysAdmin; } }
    }
    
    public class ApplicationUser : IdentityUser<int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public DateTime LastModified { get; set; }

        public bool Inactive { get; set; }
        public string FullName { get; set; }
        public string Firstname { get; set; }

        public string Lastname { get; set; }
        public DateTime? BirthDay { get; set; }


        public virtual ICollection<AD_User_Menu> AD_User_Menu { get; set; }
        public virtual ICollection<MultipleCheckboxClass> UserRole { get; set; }

        public ApplicationUser()
        {
            LastModified = DateTime.Now;
            Inactive = false;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }


        //public virtual List<ApplicationRole> UserRoles { get; set; }


        public bool IsPermissionInUserRoles(string _permission)
        {
            bool _retVal = false;
            try
            {
                foreach (ApplicationUserRole _role in this.Roles)
                {
                    if (_role.IsPermissionInRole(_permission))
                    {
                        _retVal = true;
                        break;
                    }
                }
            }
            catch (Exception)
            {
            }
            return _retVal;
        }

        public bool IsSysAdmin()
        {
            bool _retVal = false;
            try
            {
                foreach (ApplicationUserRole _role in this.Roles)
                {
                    if (_role.IsSysAdmin)                    
                    {
                        _retVal = true;
                        break;
                    }
                }
            }
            catch (Exception)
            {
            }
            return _retVal;
        }
    }

    public class ApplicationRole : IdentityRole<int, ApplicationUserRole>
    {
        public ApplicationRole()
        {
            //this.Id = Guid.NewGuid().ToString();
        }
        public ApplicationRole(string name)
            : this()
        {
            this.Name = name;
        }

        public ApplicationRole(string name, string description)
            : this(name)
        {
            this.RoleDescription = description;
        }

        public DateTime LastModified { get; set; }
        public bool IsSysAdmin { get; set; }
        public string RoleDescription { get; set; }

        public virtual ICollection<PERMISSION> PERMISSIONS { get; set; }    

        public bool IsPermissionInRole(string _permission)
        {
            bool _retVal = false;
            try
            {
                foreach (PERMISSION _perm in this.PERMISSIONS)
                {
                    if (_perm.PermissionDescription == _permission)
                    {
                        _retVal = true;
                        break;
                    }
                }
            }
            catch (Exception)
            {
            }
            return _retVal;
        }
    }
    [Table("PERMISSIONS")]
    public class PERMISSION
    {
        [Key]
        public int PermissionId { get; set; }

        [Required]
        [StringLength(50)]
        public string PermissionDescription { get; set; }


        public virtual ICollection<ApplicationRole> ROLES { get; set; }
    }

     public class PermissionRole
    {
        [Key]
        [Column(Order = 0)]
        public int RoleId { get; set; }
        [Key]
        [Column(Order = 1)]
        public int PermissionId { get; set; }

        [ForeignKey("RoleId")]
        public virtual  ApplicationRole  Role { get; set; }
        [ForeignKey("PermissionId")]
        public virtual PERMISSION Permission { get; set; }

    }

    #region TOTP4Email  

    public class VerifyOTPPhoneViewModel
    {
        public int UserId { get; set; }
        public string PhoneNumber { get; set; }

        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

    }
    public class TOTP4EmailViewModel
    {
        public int UserId { get; set; }
        [Required]
        public string Provider { get; set; }
    }

    public class TOTP4EmailViewModelGet : TOTP4EmailViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class TOTP4EmailViewModelPost : TOTP4EmailViewModel
    {
        [Required]
        [Display(Name = "Security PIN")]
        public string SecurityPIN { get; set; }
    }
    #endregion

}