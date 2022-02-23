using CupOnlineAPI.Context;
using CupOnlineAPI.Models;
using Dapper;
using Dapper.Contrib.Extensions;

namespace CupOnlineAPI.Repositories
{
    public class OrderRepository
    {
        private readonly DapperContext _context;

        public OrderRepository(DapperContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all registered organizers
        /// </summary>
        /// <returns>List of organizers with name and id</returns>
        public IEnumerable<Organizer> GetAllOrganizers()
        {
            using (var connection = _context.CreateConnection())
            {
                return connection.GetAll<Organizer>().ToList();
            }
        }


        /// <summary>
        /// Get all registered sports
        /// </summary>
        /// <returns>List of sports with name and id</returns>
        public IEnumerable<Sport> GetAllSports()
        {
            using (var connection = _context.CreateConnection())
            {
                return connection.GetAll<Sport>().ToList();

            }
        }

        /// <summary>
        /// Create new city
        /// </summary>
        /// <param name="city">City to insert in table</param>
        /// <returns>created city</returns>
        public async Task<City> CreateCity(string city)
        {
            var query = @"INSERT INTO td_cities
                        (city_name) 
                        VALUES (@city_name)
                        SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            parameters.Add("city_name", city);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);

                var createdCity = new City
                {
                    Id = id,
                    Name = city,
                };
                return createdCity;
            }
        }

        /// <summary>
        /// Creates a new organizer
        /// </summary>
        /// <param name="organizer">organizer to create</param>
        /// <returns>The new organizer</returns>
        public async Task<Organizer> CreateOrganizer(Organizer organizer)
        {
            var query = @"INSERT INTO td_clubs
                        (club_name, club_shortname, club_url, club_city_id, club_sport_id, club_status) 
                        VALUES (@clubname, @clubname, @url, @cityId, @sportId, @status) 
                        SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            parameters.Add("clubname", organizer.club_name);
            parameters.Add("clubname", organizer.club_name);
            parameters.Add("url", organizer.club_url);
            parameters.Add("cityId", organizer.club_city_id);
            parameters.Add("sportId", organizer.club_sport_id);
            parameters.Add("status", organizer.club_status);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);

                var createdOrganizer = new Organizer
                {
                    club_id = id,
                    club_name = organizer.club_name,
                    club_shortname = organizer.club_shortname,
                    club_url = organizer.club_url,
                    club_city_id=organizer.club_city_id,
                    club_sport_id=organizer.club_sport_id,
                    club_status=organizer.club_status,
                };
                return createdOrganizer;
            }
        }

        /// <summary>
        /// create a new cup
        /// </summary>
        /// <param name="cup">Details about cup from form</param>
        /// <returns></returns>
        public async Task<OrderCup> CreateCup(OrderCup cup)
        {
            var query = @"INSERT INTO td_cups
                        (cup_club_id, cup_sport_id, cup_logotype, cup_sponsor_logotype, cup_sponsor_url, cup_url, cup_date, cup_startdate, 
                        cup_enddate, cup_name, cup_players_age, cup_groups, cup_periods, cup_periodtime, cup_play_place, cup_round, cup_game_no,
                        cup_gamewin_points, cup_table_sort, cup_show_teammembers, cup_game_report, cup_sponsors, cup_status, cup_binStatus, 
                        cup_gamewinsd_points, cup_gamewinpenalties_points, cup_gamewinextra_points)
                        VALUES (@cup_club_id, @cup_sport_id, @cup_logotype, @cup_sponsor_logotype, @cup_sponsor_url, @cup_url, @cup_date, @cup_startdate, 
                        @cup_enddate, @cup_name, @cup_players_age, @cup_groups, @cup_periods, @cup_periodtime, @cup_play_place, @cup_round, @cup_game_no,
                        @cup_gamewin_points, @cup_table_sort, @cup_show_teammembers, @cup_game_report, @cup_sponsors, @cup_status, @cup_binStatus, 
                        @cup_gamewinsd_points, @cup_gamewinpenalties_points, @cup_gamewinextra_points)
                        SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            parameters.Add("cup_club_id", cup.id);
            parameters.Add("cup_sport_id", cup.sport.sport_id);
            parameters.Add("cup_logotype", "");
            if (cup.cupType == 1)
            {
                parameters.Add("cup_sponsor_logotype", "logotype_coreit.gif");
                parameters.Add("cup_sponsor_url", "http://www.coreit.se");
            }
            else
            {
                parameters.Add("cup_sponsor_logotype", "");
                parameters.Add("cup_sponsor_url", "");
            }                     
            parameters.Add("cup_url", "");
            parameters.Add("cup_date", cup.date);
            parameters.Add("cup_startdate", cup.startdate);
            parameters.Add("cup_enddate", cup.enddate);
            parameters.Add("cup_name", cup.name);
            parameters.Add("cup_players_age", cup.age);
            parameters.Add("cup_groups", 1);
            parameters.Add("cup_periods", 1);
            parameters.Add("cup_periodtime", 1);
            parameters.Add("cup_play_place", cup.organizer.club_id);
            parameters.Add("cup_round", 1);
            parameters.Add("cup_game_no", 1);            
            parameters.Add("cup_table_sort", "temp_group_team_points DESC, temp_group_team_sort_number DESC, temp_group_team_plus_minus DESC, temp_group_team_score_forward DESC");
            parameters.Add("cup_show_teammembers", 1);
            parameters.Add("cup_game_report", 1);
            parameters.Add("cup_sponsors", 1);
            parameters.Add("cup_status", 0);
            parameters.Add("cup_binStatus", 0);
            parameters.Add("cup_gamewin_points", 2);
            parameters.Add("cup_gamewinsd_points", 2);
            parameters.Add("cup_gamewinpenalties_points", 2);
            parameters.Add("cup_gamewinextra_points", 0);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);

                var createdCup = new OrderCup
                {
                    id = id,
                    organizer = cup.organizer,
                    name = cup.name,
                    sport = cup.sport,
                    city = cup.city,
                    age = cup.age,
                    date = cup.date,
                    startdate = cup.startdate,
                    enddate = cup.enddate,
                };
                return createdCup;
            }
        }
    }
}
