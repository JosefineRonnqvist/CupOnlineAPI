using CupOnlineAPI.Context;
using CupOnlineAPI.Models;
using Dapper;
using Dapper.Contrib.Extensions;

namespace CupOnlineAPI.Repositories
{
    public class SportRepository
    {
        private readonly DapperContext _context;

        public SportRepository(DapperContext context)
        {
            _context = context;
        }
        public IEnumerable<Sport> GetAll()
        {
            using (var connection = _context.CreateConnection())
            {
                return connection.GetAll<Sport>().ToList();

            }
        }

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
    }
}
