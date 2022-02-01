using Microsoft.AspNetCore.Mvc;
using CupOnlineAPI.Repositories;

namespace CupOnlineAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CupController : ControllerBase
    {
        private readonly CupRepository _cupRepo;
        public CupController(CupRepository cupRepo)
        {
            _cupRepo = cupRepo;
        }

        /// <summary>
        /// Finds cups from Cuponline
        /// </summary>
        /// <param name="noOfCups">Number of found cups</param>
        /// <returns>Cups</returns>
        [HttpGet]
        public async Task<IActionResult> Cups(int noOfCups=100)
        {
            try
            {
                var cups=await _cupRepo.GetCups(noOfCups);
                return Ok(cups);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Finds cups that start the coming 30 days
        /// </summary>
        /// <param name="noOfCups">Number of cups wanted in searchresult</param>
        /// <param name="daysFromToday">Days from today wanted in searchresult</param>
        /// <returns>cups</returns>
        [HttpGet]
        public async Task<IActionResult> Coming(int noOfCups=20, int daysFromToday = 30)
        {
            try
            {
                var cups = await _cupRepo.GetComing(noOfCups, daysFromToday);
                return Ok(cups);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Finds cups that is active now
        /// </summary>
        /// <param name="noOfCups">Number of cups wanted in searchresult</param>
        /// <returns>Cups</returns>
        [HttpGet]
        public async Task<IActionResult> Ongoing(int noOfCups=20)
        {
            try
            {
                var cups = await _cupRepo.GetOngoing(noOfCups);
                return Ok(cups);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Finds cups that ended the last 30 days
        /// </summary>
        /// <param name="noOfCups">Number of cups wanted in searchresult</param>
        /// <param name="daysFromToday">Days from today wanted in searchresult</param>
        /// <returns>Cups</returns>
        [HttpGet]
        public async Task<IActionResult> Finished(int noOfCups=20, int daysFromToday=30)
        {
            try
            {
                var cups = await _cupRepo.GetFinished(noOfCups, daysFromToday);
                return Ok(cups);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        //[HttpGet]
        //public async Task<IActionResult> Search()
        //{
        //    return await Search(100);

        //}
        //[HttpGet]
        //public async Task<IActionResult> Search(string name = "")
        //{
        //    return await Search(100,name);

        //}

        /// <summary>
        /// Finds cups that matches search
        /// </summary>
        /// <param name="noOfCups">Number of cups wanted in searchresult</param>
        /// <param name="name">Name of cup</param>
        /// <param name="year">Year when cup is played</param>
        /// <param name="organizer">The sport club that organize the cup</param>
        /// <param name="place">Place where cup is</param>
        /// <param name="sport">Sport</param>
        /// <param name="age">Age</param>
        /// <param name="status">Status</param>
        /// <returns>Cups</returns>
        [HttpGet]        
        public async Task<IActionResult> Search(int noOfCups=1000, string name="", string year= "", string organizer = "", string city = "",
                                                      int sport_id =0, int age_id=0, int status=4)
        {
            try
            {
                var cups = await _cupRepo.Search(noOfCups,name, year, organizer, city, sport_id, age_id, status);
                return Ok(cups);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
