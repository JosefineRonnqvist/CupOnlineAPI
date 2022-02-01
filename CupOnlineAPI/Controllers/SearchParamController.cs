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

        /// <summary>
        /// Get all sport from cuponline with GetAll-method (no query)
        /// </summary>
        /// <returns>List of searchparam with sports</returns>
        /// <exception cref="Exception"></exception>
        [HttpGet]
        public IEnumerable<SearchParam> GetAllSports()
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

        /// <summary>
        /// Get all sports from cuponline
        /// </summary>
        /// <returns>List of searchparam with sports, ordered with selected most popular first</returns>
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

        /// <summary>
        /// Get all years registered at cuponline
        /// </summary>
        /// <returns>List of searchparam with years</returns>
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

        /// <summary>
        /// Get ages to use in cup search
        /// </summary>
        /// <returns>List of searchparam with ages</returns>
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
