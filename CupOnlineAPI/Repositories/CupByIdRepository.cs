using CupOnlineAPI.Context;
using CupOnlineAPI.Models;
using Dapper.Contrib.Extensions;

namespace CupOnlineAPI.Repositories
{
    public class CupByIdRepository
    {
        private readonly DapperContext _context;

        public CupByIdRepository(DapperContext context)
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
    }
}
