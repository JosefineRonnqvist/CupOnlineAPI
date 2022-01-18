using Dapper.Contrib.Extensions;

namespace CupOnlineAPI.Models
{
    [Table("td_sports")]
    public class Sport
    {
        [Key]
        public int sport_id { get; set; }
        public string sport_name { get; set; }
    }
}
