﻿using Dapper.Contrib.Extensions;

namespace CupOnlineAPI.Models
{
    [Table("td_sports")]
    public class SearchParam
    {
        [Key]
        public int sport_id { get; set; }
        public string sport_name { get; set; }
        public string year { get; set; }
        public string age { get; set; }
        public string status { get; set; }
        public int age_id { get; set; }
    }
}
