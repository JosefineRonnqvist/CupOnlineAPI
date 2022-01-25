using CupOnlineAPI.Models;
using CupOnlineAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CupOnlineAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class SearchParamController : ControllerBase
    {
        private readonly SearchParamRepository _searchParamRepo;

        public SearchParamController(SearchParamRepository searchParam)
        {
            _searchParamRepo = searchParam;
        }

        [HttpGet]
        public IEnumerable<SearchParam> GetAll()
        {
            try
            {
                return _searchParamRepo.GetAllSports();
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
                var sports = await _searchParamRepo.GetSports();
                return Ok(sports);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Years()
        {
            try
            {
                var years = await _searchParamRepo.Years();
                return Ok(years);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Ages()
        {
            try
            {
                var ages = await _searchParamRepo.Ages();
                return Ok(ages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
