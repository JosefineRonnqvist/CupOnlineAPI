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
        private readonly ICupRepository _cupRepo;

        public CupController(ICupRepository cupRepo)
        {
            _cupRepo = cupRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Cups(int nrOfCups=100)
        {
            try
            {
                var cups=await _cupRepo.GetCups(nrOfCups);
                return Ok(cups);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Coming(int nrOfCups=15)
        {
            try
            {
                var cups = await _cupRepo.GetComing(nrOfCups);
                return Ok(cups);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Ongoing(int nrOfCups=15)
        {
            try
            {
                var cups = await _cupRepo.GetOngoing(nrOfCups);
                return Ok(cups);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Finished(int nrOfCups=15)
        {
            try
            {
                var cups = await _cupRepo.GetFinished(nrOfCups);
                return Ok(cups);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> Search(int nrOfCups=100, string name="", string year="", string organizer = "", string place = "",
                                                      string sport = "", string age = "")
        {
            try
            {
                var cups = await _cupRepo.Search(nrOfCups,name, year, organizer, place, sport, age);
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
