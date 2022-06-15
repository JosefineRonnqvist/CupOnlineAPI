namespace CupOnlineAPI.Models
{
    public class ConfirmationMailDetails
    {
        public string cup_user_password { get; set; }
        public string cup_user_username { get; set; }
        public int cup_id { get; set; }
        public string cup_name { get; set; }
        public string toMail { get; set; }
        public string fromMail { get; set; } = "support@cuponline.se";
    }
}
