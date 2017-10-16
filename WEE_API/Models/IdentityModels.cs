using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using WEE_API.Models;

namespace WEE_API.Models
{ 
    public class ApplicationUser : IdentityUser
    {
        DBContext _dbContext = new DBContext();
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here

            //var userRoles = userIdentity.Claims
            //   .Where(c => c.Type == ClaimTypes.Role)
            //   .Select(c => c.Value);

            //var roleClaims = _dbContext.AspNetRoleClaims
            //       .Where(x => userRoles.Any(role => role == x.Role.Id)).ToList()
            //        .Select(x => new Claim(x.ClaimType, x.ClaimValue));

            return userIdentity;
        }
    }
    public class AspNetRoleClaim
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public IdentityRole Role { get; set; }
    } 
    
}