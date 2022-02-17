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

        //public async Task<OrderCup> CreateCup(OrderCup cup)
        //{
        //    var query = @"INSERT INTO td_cups
        //                (cup_club_id, cup_name, cup_sport_id, cup_play_place, cup_players_age, cup_startdate, cup_enddate)
        //                VALUES (@organizer_id, @name, @sport_id, @city, @age, @startdate, @enddate)
        //                SELECT CAST(SCOPE_IDENTITY() as int)";

        //    var parameters = new DynamicParameters();
        //    parameters.Add("organizer_id", cup.organizer.club_id);
        //    parameters.Add("name", cup.name);
        //    parameters.Add("sport_id", cup.sport.sport_id);
        //    parameters.Add("city", cup.city);
        //    parameters.Add("age", cup.age);
        //    parameters.Add("startdate", cup.startdate);
        //    parameters.Add("enddate", cup.enddate);

        //    using (var connection = _context.CreateConnection())
        //    {
        //        var id = await connection.QuerySingleAsync<int>(query, parameters);

        //        var createdCup = new OrderCup
        //        {
        //            id = id,
        //            organizer = cup.organizer,
        //            name = cup.name,
        //            sport = cup.sport,
        //            city = cup.city,
        //            age = cup.age,
        //            startdate = cup.startdate,
        //            enddate = cup.enddate,
        //        };
        //        return createdCup;
        //    }
        //}
    }
}
