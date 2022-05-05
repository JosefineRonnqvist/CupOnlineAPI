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
        public async Task<int> CreateCup(OrderCup cup, int cupType)
        {
            using (var connection = _context.CreateConnection())
            {
                var newCup = new OrderCup
                {
                    cup_club_id = cup.cup_club_id,
                    cup_sport_id = cup.cup_sport_id,
                    cup_sponsor_logotype = CheckLogotype(cupType),
                    cup_sponsor_url = CheckUrl(cupType),
                    cup_date = cup.cup_date,
                    cup_startdate = cup.cup_startdate,
                    cup_enddate = cup.cup_enddate,
                    cup_name= cup.cup_name,
                    cup_players_age = cup.cup_players_age,
                    cup_play_place= cup.cup_play_place,
                };
                return await connection.InsertAsync(newCup);
            }
        }

        public string CheckLogotype(int cupType)
        {
            if (cupType == 1)
            {
                return "logotype_coreit.gif";
            }
            return "";
        }

        public string CheckUrl(int cupType)
        {
            if (cupType == 1)
            {
                return "http://www.coreit.se";
            }
            return "";
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

        public async Task<int> CreateCupAdmin(OrderAdmin admin)
        {
            using (var connection = _context.CreateConnection())
            {
                var newAdmin = new OrderAdmin
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
