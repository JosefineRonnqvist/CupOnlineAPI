using Microsoft.AspNetCore.Mvc;
using CupOnlineAPI.Repositories;
using CupOnlineAPI.Models;
using System.Runtime.InteropServices;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        public async Task<IActionResult> Coming(int noOfCups=15, int daysFromToday = 30)
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
        public async Task<IActionResult> Ongoing(int noOfCups=15)
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
        public async Task<IActionResult> Finished(int noOfCups=15, int daysFromToday=30)
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
        /// <returns>Cups</returns>
        [HttpGet]
        public async Task<IActionResult> Search(int noOfCups=200, string? name="", string? year="", string? organizer = "", string? place = "",
                                                      string? sport = "", int? age=0, int? status=0)
        {
            try
            {
                var cups = await _cupRepo.Search(noOfCups,name, year, organizer, place, sport, age, status);
                return Ok(cups);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }




        //[HttpGet]
        //public IEnumerable<Cup> GetAllCups()
        //{
        //    try
        //    {
        //        return _cupRepo.GetAllCups();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //// POST api/<CupController>
        //[HttpPost]
        //public async Task<IActionResult> CreateCup(Cup cup)
        //{
        //    try
        //    {
        //        var createdCup = await _cupRepo.CreateCup(cup);
        //        return Ok(createdCup);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}

        //// PUT api/<CupController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<CupController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
