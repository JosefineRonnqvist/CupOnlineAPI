using CupOnlineAPI.Interfaces;
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
        private readonly ICupByIdRepository _cupByIdRepo;

        public CupByIdController(ICupByIdRepository cupByIdRepo)
        {
            _cupByIdRepo = cupByIdRepo;
        }

        [HttpGet("{id}")]
        public CupById CupById(int id)
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
