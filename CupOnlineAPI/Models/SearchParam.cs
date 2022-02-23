using System.ComponentModel.DataAnnotations;

namespace CupOnlineAPI.Models
{
    public class SearchParam
    {
        [Key]
        public Sport sport { get; set; }
        public string year { get; set; }
        public string age { get; set; }
        public string status { get; set; }
        public int age_id { get; set; }
        public string organizer { get; set; }
        public City city { get; set; }

    }
}
