
using CupOnlineAPI.Models;
using CupOnlineAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CupOnlineAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class CupByIdController : ControllerBase
    {
        private readonly CupByIdRepository _cupByIdRepo;

        public CupByIdController(CupByIdRepository cupByIdRepo)
        {
            _cupByIdRepo = cupByIdRepo;
        }

        [HttpGet("{id}")]
        public CupById Find(int id)
        {
            try
            {
                var cup = _cupByIdRepo.GetCupById(id);
                //if(cup==null)
                //return NotFound();
                return cup;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
