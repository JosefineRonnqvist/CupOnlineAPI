using CupOnlineAPI.Context;
using CupOnlineAPI.Interfaces;
using CupOnlineAPI.Models;
using Dapper.Contrib.Extensions;

namespace CupOnlineAPI.Repositories
{
    public class CupByIdRepository:ICupByIdRepository 
    {
        private readonly DapperContext _context;

        public CupByIdRepository(DapperContext context)
        {
            _context = context;
        }
        public CupById GetCupById(int? id)
        {
            using (var connection = _context.CreateConnection())
            {
                return connection.Get<CupById>(id);
            }
        }
    }
}
