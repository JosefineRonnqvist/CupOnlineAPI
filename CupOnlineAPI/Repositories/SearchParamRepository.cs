using CupOnlineAPI.Context;
using CupOnlineAPI.Models;
using Dapper;
using Dapper.Contrib.Extensions;

namespace CupOnlineAPI.Repositories
{
    public class SearchParamRepository
    {
        private readonly DapperContext _context;

        public SearchParamRepository(DapperContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all registered cups, ordered with 
        /// </summary>
        /// <returns>List of sports with name and id</returns>
        public async Task<IEnumerable<Sport>> GetSports()
        {
            var query = @"SELECT sport_id, sport_name from td_sports 
                        ORDER BY case 
                        WHEN sport_id in (1,2,3) 
                        THEN sport_id
                        ELSE 100 
                        END,sport_name";
            using (var connection = _context.CreateConnection())
            {
                var cups = await connection.QueryAsync<Sport>(query);
                return cups.ToList();
            }
        }

        /// <summary>
        /// get all registered years from start date
        /// </summary>
        /// <returns>List of years</returns>
        public async Task<IEnumerable<SearchParam>> GetYears()
        {
            var query = @"SELECT DISTINCT LEFT (cup_startdate,4) AS year
                        FROM td_cups
                        ORDER BY year DESC";
            using (var connection = _context.CreateConnection())
            {
                var searchParam = await connection.QueryAsync<SearchParam>(query);
                return searchParam.ToList();
            }
        }

        /// <summary>
        /// Calculate ages
        /// </summary>
        /// <returns>list of age cathegory</returns>
        public async Task<IEnumerable<SearchParam>> GetAges()
        {
            var query = @"declare @LID as int =1
                        SELECT td_ages.ID AS age_id, td_ages.year, td_ages.status, ISNULL(l.localName, lEng.localName) + 
                        CASE WHEN status < 3 THEN ' ' + CONVERT(nvarchar(4), [year]) ELSE '' END AS age
                        FROM td_ages LEFT OUTER JOIN
                        td_localization AS l ON l.languageId = @LID AND l.property = 'gender_status_' + 
                        CONVERT(nvarchar(1), td_ages.status) LEFT OUTER JOIN
                        td_localization AS lEng ON lEng.languageId = 2 AND lEng.property = 'gender_status_' + 
                        CONVERT(nvarchar(1), td_ages.status)
                        WHERE (td_ages.year < YEAR(GETDATE()) - 4) AND (td_ages.year > YEAR(GETDATE()) - 21) OR
                        (td_ages.year = 9999)
                        ORDER BY td_ages.status, td_ages.year";
            using (var connection = _context.CreateConnection())
            {
                var searchParam = await connection.QueryAsync<SearchParam>(query);
                return searchParam.ToList();
            }
        }

        /// <summary>
        /// Get all registered cities 
        /// </summary>
        /// <returns>List of cities</returns>
        public async Task<IEnumerable<City>> GetCities(string city)
        {
            var query = @"SELECT DISTINCT (c.city_name), outter_c.city_id
                        FROM td_cities AS c
						OUTER APPLY (SELECT TOP(1) *
						FROM td_cities AS inner_c
                        WHERE inner_c.city_name =c.city_name)
						AS outter_c";
            using (var connection = _context.CreateConnection())
            {
                var City = await connection.QueryAsync<City>(query, new
                {
                    city = "%" + city + "%"
                });
                return City.ToList();
            }
        }

        public async Task<IEnumerable<Organizer>> GetOrganizers(string clubName)
        {
            var query = @"SELECT DISTINCT club_id, club_name
                        FROM td_clubs
                        WHERE club_name LIKE @clubName
                        ORDER BY club_name ASC";
            using (var connection = _context.CreateConnection())
            {
                var organizer = await connection.QueryAsync<Organizer>(query, new
                {
                    clubName= "%"+clubName+"%"
                } );
                return organizer.ToList();
            }
        }

    }
}
