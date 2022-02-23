using Dapper.Contrib.Extensions;

namespace CupOnlineAPI.Models
{
    [Table("td_cities")]
    public class City
    {
        [Key]
        public int city_id { get; set; }
        public string city_name { get; set; }
        
    }
}
