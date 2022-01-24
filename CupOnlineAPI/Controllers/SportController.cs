using CupOnlineAPI.Models;
using CupOnlineAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CupOnlineAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class SportController : ControllerBase
    {
        private readonly SportRepository _sportRepo;

        public SportController(SportRepository sportRepo)
        {
            _sportRepo = sportRepo;
        }

        [HttpGet]
        public IEnumerable<Sport> GetAll()
        {
            try
            {
                return _sportRepo.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Sports()
        {
            try
            {
                var sports = await _sportRepo.GetSports();
                return Ok(sports);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
