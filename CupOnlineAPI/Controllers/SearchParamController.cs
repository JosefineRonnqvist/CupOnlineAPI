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
                var years = await _searchParamRepo.GetYears();
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
                var ages = await _searchParamRepo.GetAges();
                return Ok(ages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Get all cities from cuponline
        /// </summary>
        /// <returns>List of searchparam with cities</returns>
        [HttpGet]
        public async Task<ActionResult> Cities(string city)
        {
            try
            {
                var cities = await _searchParamRepo.GetCities(city);
                return Ok(cities);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clubName"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Organizers(string clubName)
        {
            try
            {
                var organizers = await _searchParamRepo.GetOrganizers(clubName);
                return Ok(organizers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
