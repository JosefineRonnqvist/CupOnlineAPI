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
        
        //public IEnumerable<Cup> GetAllCups()
        //{
        //    using (var connection = _context.CreateConnection())
        //    {
        //        return connection.GetAll<Cup>().ToList();

        //    }
        //}

        //public async Task CreateCup(Cup cup)
        //{
        //    var query = @"insert into [td_cups] (cup_id as Id, cup_name as Name, cup_players_age as Players_age, cup_play_place as Play_place)
        //                  values (@Id, @Name, @Players_age, @Play_place)";
        //    var parameters = new DynamicParameters();
        //    parameters.Add("Name", cup.Name, DbType.String);
        //    parameters.Add("Players_age", cup.Players_age, DbType.String);
        //    parameters.Add("Play_place", cup.Play_place, DbType.String);
        //    using (var connection = _context.CreateConnection())
        //    {
        //        await connection.ExecuteAsync(query, parameters);
        //    }
        //}

        //public Cup GetCupById(int id)
        //{
        //    //var query = @"select cup_id as Id, cup_name as Name, cup_players_age as Players_age, cup_play_place as Play_place 
        //    //            from[td_cups] 
        //    //            where cup_id=@Id";
        //    using (var connection = _context.CreateConnection())
        //    {
        //        return connection.Get<Cup>(id);
        //        //var cup = await connection.QuerySingleOrDefaultAsync<Cup>(query, new { id });
        //        //return cup;
        //    }
        //}

        public async Task<IEnumerable<Cup>> GetCups(int? noOfCups)
        {
            var query = @"SET ROWCOUNT @noOfCups
                          SELECT cup_id AS id, cup_name AS name, cup_players_age AS players_age, cup_url,
                          cup_play_place AS play_place
                          FROM td_cups";
            using (var connection = _context.CreateConnection())
            {
                var cups = await connection.QueryAsync<Cup>(query, new { noOfCups = noOfCups });
                return cups.ToList();
            }
        }

        public async Task<IEnumerable<Cup>> GetComing(int? noOfCups)
        {
            var query = @"SET ROWCOUNT @noOfCups
                        SELECT cup_id AS id,cup_date AS date, cup_name AS name, cup_startdate, cup_enddate, sport_name, cup_url
                        FROM td_cups
                        INNER JOIN td_sports ON cup_sport_id=sport_id
                        WHERE cup_startdate BETWEEN @today AND @today_plus30
                        ORDER BY cup_startdate ASC";
            using (var connection = _context.CreateConnection())
            {
                var cups = await connection.QueryAsync<Cup>(query, new
                {
                    today = DateTime.Now.ToString("yyyy-MM-dd"),
                    today_plus30 = DateTime.Now.AddDays(30).ToString("yyyy-MM-dd"),
                    noOfCups = noOfCups
                });
                return cups.ToList();
            }
        }

        public async Task<IEnumerable<Cup>> GetOngoing(int? noOfCups)
        {
            var query = @"SET ROWCOUNT @noOfCups
                        SELECT cup_id AS id,cup_date AS date, cup_name AS name, cup_startdate, cup_enddate, sport_name, cup_url
                        FROM td_cups
                        INNER JOIN td_sports ON cup_sport_id=sport_id
                        WHERE cup_startdate < @today 
                        AND cup_enddate > @today
                        ORDER BY cup_startdate DESC";
            using (var connection = _context.CreateConnection())
            {
                var cups = await connection.QueryAsync<Cup>(query, new
                {
                    today = DateTime.Now.ToString("yyyy-MM-dd"),
                    noOfCups = noOfCups
                });
                return cups.ToList();
            }
        }

        public async Task<IEnumerable<Cup>> GetFinished(int? noOfCups)
        {
            var query = @"SET ROWCOUNT @noOfCups
                        SELECT cup_id AS id,cup_date AS date, cup_name AS name, cup_startdate, cup_enddate, sport_name, cup_url
                        FROM td_cups
                        INNER JOIN td_sports ON cup_sport_id=sport_id
                        WHERE cup_enddate BETWEEN @today_minus30 AND @today
                        ORDER BY cup_enddate DESC";
            using (var connection = _context.CreateConnection())
            {
                var cups = await connection.QueryAsync<Cup>(query, new
                {
                    today = DateTime.Now.ToString("yyyy-MM-dd"),
                    today_minus30 = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd"),
                    noOfCups = noOfCups
                }); ;
                return cups.ToList();
            }
        }

        public async Task<IEnumerable<Cup>> Search(int? noOfCups, string name, string year, string organizer, string place,
                                                    string sport, string age)
        {
            var query = @"SET ROWCOUNT @noOfCups
                        SELECT cup_id AS id, cup_name AS name, cup_players_age AS age, 
                        cup_date AS date, cup_startdate, cup_enddate, cup_url, club_url,
                        club_name, sport_name, cup_play_place AS place
                        FROM td_cups
                        INNER JOIN td_sports ON cup_sport_id=sport_id
                        INNER JOIN td_clubs ON cup_club_id=club_id
                        WHERE cup_name LIKE @name
                        AND cup_date LIKE @year
                        AND cup_players_age LIKE @age
                        AND club_name LIKE @organizer
                        AND sport_name LIKE @sport
                        AND cup_play_place LIKE @place
                        ORDER BY cup_enddate DESC";
            using (var connection = _context.CreateConnection())
            {
                var cups = await connection.QueryAsync<Cup>(query, new
                {
                    name = "%" + name + "%",
                    age = "%" + age + "%",
                    year = "%" + year + "%",
                    organizer = "%" + organizer + "%",
                    sport = "%" + sport + "%",
                    place = "%" + place + "%",
                    noOfCups = noOfCups
                });
                return cups.ToList();
            }
        }

    }
}
