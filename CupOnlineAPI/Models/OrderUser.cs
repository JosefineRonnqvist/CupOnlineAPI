using KeyAttribute = Dapper.Contrib.Extensions.KeyAttribute;
using System.ComponentModel.DataAnnotations;
using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity;
using System.Web.Security;

namespace CupOnlineAPI.Models
{
    [Table("td_cup_users")]
    public class OrderUser
    {
        [Key]
        public int cup_user_id { get; set; }
        public string? cup_user_username { get; set; }
        public string? cup_user_password { get; set; }
        public int? cup_user_cup_id { get; set; }
        public int? cup_user_rights { get; set; }
        [Required]
        public string? cup_user_name { get; set; }
        [Required]
        [EmailAddress]
        public string? cup_user_email { get; set; }
        [Required]
        [Phone]
        public string? cup_user_phone { get; set; }

        void CreatePassword()
        {
            var hasher = new PasswordHasher();
            var password = Membership.GeneratePassword(8, 3);
        }
    }
}
