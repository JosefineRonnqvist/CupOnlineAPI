using Dapper.Contrib.Extensions;

namespace CupOnlineAPI.Models
{
    [Table("td_cup_users")]
    public class OrderAdmin
    {
        [Key]
        public int cup_user_id { get; set; }
        public string cup_user_username { get; set; }
        public string cup_user_password { get; set; }
        public int cup_user_cup_id { get; set; }
        public int cup_user_rights { get; set; }
        public string cup_user_name { get; set; }
        public string cup_user_email { get; set; }
        public string cup_user_phone { get; set; }
    }
}
