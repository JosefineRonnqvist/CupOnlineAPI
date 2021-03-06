using Dapper.Contrib.Extensions;

namespace CupOnlineAPI.Models
{

    [Table("td_clubs")]
    public class Organizer
    {
        [Key]
        public int club_id { get; set; }
        public string club_name { get; set; }
        public string club_shortname { get; set; }
        public string club_url { get; set; }
        public int club_city_id { get; set; }

        public int club_sport_id { get; set; }

        public int club_status { get; set; } = 1;

    }
}
