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
        public async Task<IEnumerable<Organizer>> GetAllOrganizers()
        {
            var query = @"SELECT club_id, club_name from td_clubs 
                        ORDER BY club_name ASC";
            using (var connection = _context.CreateConnection())
            {
                var cups = await connection.QueryAsync<Organizer>(query);
                return cups.ToList();
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
                return connection.GetAll<Sport>().OrderBy(s=>s.sport_name);
            }
        }

        /// <summary>
        /// Create new city
        /// </summary>
        /// <param name="city">City to insert in table</param>
        /// <returns>Created city id</returns>
        public async Task<int> CreateCity(City city)
        {
            using (var connection = _context.CreateConnection())
            {
                var newCity = new City
                {
                    city_name = city.city_name,
                };
                return await connection.InsertAsync(newCity);
            }
        }

        /// <summary>
        /// Creates a new organizer
        /// </summary>
        /// <param name="organizer">organizer to create</param>
        /// <returns>Created organizer id</returns>
        public async Task<int> CreateOrganizer(Organizer organizer)
        {
            using (var connection = _context.CreateConnection())
            {
                var newOrganizer = new Organizer
                {
                    club_name = organizer.club_name,
                    club_shortname = organizer.club_shortname,
                    club_url = organizer.club_url,
                    club_city_id = organizer.club_city_id,
                    club_sport_id = organizer.club_sport_id,
                    club_status = organizer.club_status,
                };
                return await connection.InsertAsync(newOrganizer);
            }
        }

        /// <summary>
        /// create a new cup
        /// </summary>
        /// <param name="cup">Details about cup from form</param>
        /// <returns>Created cup id</returns>
        public async Task<int> CreateCup(OrderCup cup)
        {
            using (var connection = _context.CreateConnection())
            {
                var newCup = new OrderCup
                {
                    cup_club_id = cup.cup_club_id,
                    cup_sport_id = cup.cup_sport_id,
                    cup_logotype= cup.cup_logotype,
                    cup_sponsor_logotype = cup.cup_sponsor_logotype,
                    cup_sponsor_url = cup.cup_sponsor_url,
                    cup_url=cup.cup_url,
                    cup_date = cup.cup_date,
                    cup_startdate = cup.cup_startdate,
                    cup_enddate = cup.cup_enddate,
                    cup_name= cup.cup_name,
                    cup_players_age = cup.cup_players_age,
                    cup_groups=cup.cup_groups,
                    cup_periods=cup.cup_periods,
                    cup_periodtime=cup.cup_periodtime,
                    cup_play_place= cup.cup_play_place,
                    cup_round=cup.cup_round,
                    cup_game_no=cup.cup_game_no,
                    cup_table_sort=cup.cup_table_sort,
                    cup_show_teammembers=cup.cup_show_teammembers,
                    cup_game_report=cup.cup_game_report,
                    cup_sponsors=cup.cup_sponsors,
                    cup_status=cup.cup_status,
                    cup_binStatus=cup.cup_binStatus,
                    cup_gamewin_points=cup.cup_gamewin_points,
                    cup_gamedraw_points=cup.cup_gamedraw_points,
                    cup_gamewinsd_points=cup.cup_gamewinsd_points,
                    cup_gamewinpenalties_points=cup.cup_gamewinpenalties_points,
                    cup_gamewinextra_points=cup.cup_gamewinextra_points
                };
                return await connection.InsertAsync(newCup);
            }
        }

        public async Task<int> CreateCupRegistration(OrderRegistration reg)
        {
            using (var connection = _context.CreateConnection())
            {
                var newOrderReg = new OrderRegistration
                {
                    cup_id = reg.cup_id,
                    message = reg.message,
                    invoiceAddress = reg.invoiceAddress,
                    registrationDate = reg.registrationDate,
                    payDate = reg.payDate,
                    orderStatus = reg.orderStatus,
                    foundType = reg.foundType,
                    regIp = reg.regIp,
                    status = reg.status,
                    payAmount = reg.payAmount,
                };
                return await connection.InsertAsync(newOrderReg);
            }
        }

        public async Task<int> CreateCupAdmin(OrderUser admin)
        {
            using (var connection = _context.CreateConnection())
            {
                var newAdmin = new OrderUser
                {
                    cup_user_username = admin.cup_user_username,
                    cup_user_password = admin.cup_user_password,
                    cup_user_cup_id = admin.cup_user_cup_id,
                    cup_user_rights = admin.cup_user_rights,
                    cup_user_name = admin.cup_user_name,
                    cup_user_email = admin.cup_user_email,
                    cup_user_phone = admin.cup_user_phone,
                };
                return await connection.InsertAsync(newAdmin);
            }
        }


    }
}
