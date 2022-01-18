using CupOnlineAPI.Context;
using CupOnlineAPI.Models;
using Dapper.Contrib.Extensions;

namespace CupOnlineAPI.Repositories
{
    public class SportRepository : ISportRepository
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
    }
}
