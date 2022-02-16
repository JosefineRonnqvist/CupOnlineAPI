using System.ComponentModel.DataAnnotations;

namespace CupOnlineAPI.Models
{
    public class SearchParam
    {
        [Key]
        public int sport_id { get; set; }
        public string sport_name { get; set; }
        public string year { get; set; }
        public string age { get; set; }
        public string status { get; set; }
        public int age_id { get; set; }
        public string organizer { get; set; }
    }
}
