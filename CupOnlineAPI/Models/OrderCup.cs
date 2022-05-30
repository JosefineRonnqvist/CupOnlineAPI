using Dapper.Contrib.Extensions;
using System.ComponentModel.DataAnnotations;
using KeyAttribute = Dapper.Contrib.Extensions.KeyAttribute;

namespace CupOnlineAPI.Models
{
    [Table("td_cups")]
    public class OrderCup
    {
        [Key]
        public int cup_id { get; set; }
        [Required]
        public int cup_club_id { get; set; }
        [Required]
        public int cup_sport_id { get; set; }
        public string cup_logotype { get; set; } = "";
        public string cup_sponsor_logotype { get; set; } 
        public string cup_sponsor_url { get; set; }
        public string cup_url { get; set; } = "";
        public string cup_date { get; set; }
        [Required]
        public string cup_startdate { get; set; }
        [Required]
        public string cup_enddate { get; set; }
        [Required]
        public string cup_name { get; set; }
        public string cup_players_age { get; set; } = "";
        public int cup_groups { get; set; } = 1;
        public int cup_periods { get; set; } = 1;
        public int cup_periodtime { get; set; } = 1;
        [Required]
        public string cup_play_place { get; set; }
        public int cup_round { get; set; } = 1;
        public int cup_game_no { get; set; } = 1;       
        public string cup_table_sort { get; set; } = "temp_group_team_points DESC, temp_group_team_sort_number DESC, temp_group_team_plus_minus DESC, temp_group_team_score_forward DESC";
        public int cup_show_teammembers { get; set; } = 1;
        public int cup_game_report { get; set; } = 1;
        public int cup_sponsors { get; set; } = 1;
        public int cup_status { get; set; } = 0;
        public int cup_binStatus { get; set; } = 0;
        public int cup_gamewin_points { get; set; } = 2;
        public int cup_gamedraw_points { get; set; } = 1;
        public int cup_gamewinsd_points { get; set; } = 2;
        public int cup_gamewinpenalties_points { get; set; } = 2;
        public int cup_gamewinextra_points { get; set; } = 0;
        public DateTime cup_changeDate { get; set; }= DateTime.Now;
        public int cup_changedBy { get; set; }
    }
}
