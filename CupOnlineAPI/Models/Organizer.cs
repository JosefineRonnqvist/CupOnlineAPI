using Dapper.Contrib.Extensions;

namespace CupOnlineAPI.Models
{

    [Table("td_clubs")]
    public class Organizer
    {
        [Key]
        public int club_id { get; set; }
        public string club_name { get; set; }
    }
}
