﻿using CupOnlineAPI.Context;
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
        public IEnumerable<SearchParam> GetAllSports()
        {
            using (var connection = _context.CreateConnection())
            {
                return connection.GetAll<SearchParam>().ToList();

            }
        }

        public async Task<IEnumerable<SearchParam>> GetSports()
        {
            var query = @"SELECT sport_id, sport_name from td_sports 
                        ORDER BY case 
                        WHEN sport_id in (1,2,3) 
                        THEN sport_id
                        ELSE 100 
                        END,sport_name";
            using (var connection = _context.CreateConnection())
            {
                var cups = await connection.QueryAsync<SearchParam>(query);
                return cups.ToList();
            }
        }


        public async Task<IEnumerable<SearchParam>> Years()
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

        public async Task<IEnumerable<SearchParam>> Ages()
        {
            var query = @"declare @LID as int =1
                        SELECT td_ages.ID, td_ages.year, td_ages.status, ISNULL(l.localName, lEng.localName) + 
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
    }
}