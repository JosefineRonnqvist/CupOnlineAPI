using Dapper.Contrib.Extensions;

namespace CupOnlineAPI.Models
{
    [Table("td_cups")]
    public class CupById
    {
        [Key]
        public int cup_id { get; set; }
        public int cup_club_id { get; set; }
        public int cup_sport_id { get; set; }
        public string cup_logotype { get; set; }
        public string cup_sponsor_logotype { get; set; }
        public string cup_sponsor_url { get; set; }
        public string cup_url { get; set; }
        public string cup_date { get; set; }
        public string cup_startdate { get; set; }
        public string cup_enddate { get; set; }
        public string cup_name { get; set; }        
        public string cup_players_age { get; set; }
        public int cup_groups { get; set; }
        public int cup_periods { get; set; }
        public int cup_periodtime { get; set; }
        public string cup_play_place { get; set; }
        public int cup_round { get; set; }
        public int cup_game_no { get; set; }
        public int cup_gamewin_points { get; set; }
        public string cup_table_sort { get; set; }
        public int cup_show_teammembers { get; set; }
        public int cup_game_report { get; set; }
        public int cup_sponsors { get; set; }
        public int cup_status { get; set; }
        public int cup_binStatus { get; set; }
        public int cup_gamedraw_points { get; set; }
        public int cup_gamewinsd_points { get; set; }
        public int cup_gamewinpenalties_points { get; set; }
        public int cup_gamewinextra_points { get; set; }
        public DateTime cup_changeDate { get; set; }
        public int cup_changedBy { get; set; }
    }
}
