namespace CupOnlineAPI.Models
{
    public class OrderMailDetails
    {
        public string acceptSharing { get; set; }
        public string invoiceAddress { get; set; }
        public string cup_user_password { get; set; }
        public string cup_user_username { get; set; }
        public string cup_user_phone { get; set; }
        public string cup_user_email { get; set; }
        public string cup_user_name { get; set; }
        public string sport { get; set; }
        public string cup_players_age { get; set; }
        public string cup_play_place { get; set; }
        public string cup_startdate { get; set; }
        public string cup_enddate { get; set; }
        public int cup_id { get; set; }
        public string organizer { get; set; }
        public string message { get; set; }
        public string cup_name { get; set; }
        public string toMail { get; set; } = "josefine.ronnqvist@coreit.se";
        public string fromMail { get; set; } = "order@cuponline.se";
    }
}
