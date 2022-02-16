using CupOnlineAPI.Context;
using CupOnlineAPI.Models;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Data;
using System.Runtime.InteropServices;

namespace CupOnlineAPI.Repositories
{
    public class CupRepository
    {
        private readonly DapperContext _context;

        public CupRepository(DapperContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Using Dapper contrib to get cup by id
        /// </summary>
        /// <param name="id">Id of cup</param>
        /// <returns>list of cups</returns>
        public CupById GetCupById(int? id)
        {
            using (var connection = _context.CreateConnection())
            {
                return connection.Get<CupById>(id);
            }
        }

        /// <summary>
        /// Get cups with start date after today
        /// </summary>
        /// <param name="noOfCups">Number of cups in searchresult</param>
        /// <param name="daysFromToday">How many days from today included</param>
        /// <returns>List of cups</returns>
        public async Task<IEnumerable<Cup>> GetComing(int? noOfCups, int daysFromToday)
        {
            var query = @"SET ROWCOUNT @noOfCups
                        SELECT cup_id AS id,cup_date AS date, cup_name AS name, cup_startdate, cup_enddate, sport_name
                        FROM td_cups
                        INNER JOIN td_sports ON cup_sport_id=sport_id
                        WHERE cup_startdate BETWEEN GETDATE() AND @today_plus
                        ORDER BY cup_startdate ASC";
            using (var connection = _context.CreateConnection())
            {
                var cups = await connection.QueryAsync<Cup>(query, new
                {
                    today_plus = DateTime.Now.AddDays(daysFromToday).ToString("yyyy-MM-dd"),
                    noOfCups = noOfCups
                });
                return cups.ToList();
            }
        }

        /// <summary>
        /// Get cups with start date before today and enddate after today
        /// </summary>
        /// <param name="noOfCups">Number of cups in searchresult</param>
        /// <returns>List of cups</returns>
        public async Task<IEnumerable<Cup>> GetOngoingCups(int? noOfCups)
        {
            var query = @"SET ROWCOUNT @noOfCups
                        SELECT cup_id AS id,cup_date AS date, cup_name AS name, cup_startdate, cup_enddate, sport_name
                        FROM td_cups
                        INNER JOIN td_sports ON cup_sport_id=sport_id
                        INNER JOIN td_clubs ON cup_club_id=club_id
                        WHERE datediff(d, cup_startdate, GETDATE())>=0
                        AND datediff(d, cup_enddate, getdate()) <=0
                        AND cup_club_id <> 5 
                        AND club_status = 1 
                        AND cup_status =1
                        AND datediff(d, cup_startdate, cup_enddate)<30
                        ORDER BY cup_startdate DESC, cup_name ASC";
            using (var connection = _context.CreateConnection())
            {
                var cups = await connection.QueryAsync<Cup>(query, new
                {
                    noOfCups = noOfCups
                });
                return cups.ToList();
            }
        }

        /// <summary>
        /// Get cups with start date before today and enddate after today
        /// </summary>
        /// <param name="noOfCups">Number of cups in searchresult</param>
        /// <returns>List of cups</returns>
        public async Task<IEnumerable<Cup>> GetOngoingSeries(int? noOfCups)
        {
            var query = @"SET ROWCOUNT @noOfCups
                        SELECT cup_id AS id,cup_date AS date, cup_name AS name, cup_startdate, cup_enddate, sport_name
                        FROM td_cups
                        INNER JOIN td_sports ON cup_sport_id=sport_id
                        WHERE cup_startdate < GETDATE() 
                        AND cup_enddate > GETDATE()
                        ORDER BY cup_startdate DESC";
            using (var connection = _context.CreateConnection())
            {
                var cups = await connection.QueryAsync<Cup>(query, new
                {
                    noOfCups = noOfCups
                });
                return cups.ToList();
            }
        }

        /// <summary>
        /// Get cups with end date before today
        /// </summary>
        /// <param name="noOfCups">Number of cups in searchresult</param>
        /// <param name="daysFromToday">How many days from today included</param>
        /// <returns>List of cups</returns>
        public async Task<IEnumerable<Cup>> GetFinished(int? noOfCups, int daysFromToday)
        {
            var query = @"SET ROWCOUNT @noOfCups
                        SELECT top 1000 cup_id AS id,cup_date AS date, cup_name AS name, cup_startdate, cup_enddate, sport_name
                        FROM td_cups
                        INNER JOIN td_sports ON cup_sport_id=sport_id
                        WHERE cup_enddate BETWEEN @today_minus AND GETDATE()
                        ORDER BY cup_enddate DESC";
            using (var connection = _context.CreateConnection())
            {
                var cups = await connection.QueryAsync<Cup>(query, new
                {
                    today_minus = DateTime.Now.AddDays(-daysFromToday).ToString("yyyy-MM-dd"),
                    noOfCups = noOfCups
                });
                return cups.ToList();
            }
        }

        /// <summary>
        /// Get cups matching search parameters
        /// </summary>
        /// <param name="noOfCups">Number of cups in searchresult</param>
        /// <param name="name">Name of cup</param>
        /// <param name="year">Year when cup is</param>
        /// <param name="organizer">Club name of club that organize cup</param>
        /// <param name="city">Place where cup is</param>
        /// <param name="sport_id">Id of sport played (get name with SearchParamRepository.getsports())</param>
        /// <param name="age_id">Id of age cathegory</param>
        /// <param name="status">Status of cup</param>
        /// <returns>List of cups</returns>
        public async Task<IEnumerable<Cup>> Search(int noOfCups, string name="", string year="", string organizer="", string city="",
                                                    int? sport_id=0, int? age_id=0, int status=4)
        {
            var query = @"SET ROWCOUNT @noOfCups
                        SELECT TOP 1000 cup_id AS id, cup_name AS name, cup_players_age AS age, 
                        cup_date AS date, cup_startdate, cup_enddate,
                        club_name AS organizer, sport_name, cup_play_place AS city
                        FROM td_cups
                        INNER JOIN td_sports ON cup_sport_id=sport_id
                        INNER JOIN td_clubs ON cup_club_id=club_id
                        WHERE ( @name ='' OR (cup_name LIKE @name) OR (cup_players_age LIKE @name) OR (cup_play_place LIKE @name) OR (club_name LIKE @name))  
                            AND ( @year='' OR cup_date LIKE @year)
                            AND ((@age_id=0) OR (cup_id IN (SELECT cup_Id FROM td_cup_ages WHERE age_id = @age_id)))
                            AND ((@organizer ='') OR (club_name LIKE @organizer))  
                            AND ((cup_sport_id = @sport_id) OR @sport_id=0)
                            AND ((@city ='') OR cup_play_place LIKE @city)
                            AND ((@status = 0) 
                                OR (@status=1 AND cup_enddate < GETDATE())
                                OR (@status=2 AND cup_startdate <= GETDATE() AND cup_enddate>=GETDATE()) 
                                OR (@status=3 AND (cup_startdate > GETDATE() OR cup_enddate>GETDATE())) 
                                OR (@status = 4 AND cup_enddate >= GETDATE()))
                        ORDER BY cup_enddate DESC";

            using (var connection = _context.CreateConnection())
            {
                var cups = await connection.QueryAsync<Cup>(query, new
                {
                    name = "%" + name.Replace("*","%").Replace("?", "_") + "%",
                    age_id = age_id,
                    year = "%" + year + "%",
                    organizer = "%" + organizer.Replace("*", "%").Replace("?", "_") + "%",
                    sport_id = sport_id,
                    city = "%" + city.Replace("*", "%").Replace("?", "_") + "%",
                    noOfCups = noOfCups,
                    status = status,
                }); 
                return cups.ToList();
            }
        }
    }
}
