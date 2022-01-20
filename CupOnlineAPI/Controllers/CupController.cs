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

        [HttpGet]
        public async Task<IActionResult> Coming(int noOfCups=15)
        {
            try
            {
                var cups = await _cupRepo.GetComing(noOfCups);
                return Ok(cups);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


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


        [HttpGet]
        public async Task<IActionResult> Finished(int noOfCups=15)
        {
            try
            {
                var cups = await _cupRepo.GetFinished(noOfCups);
                return Ok(cups);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> Search(int noOfCups=200, string name="", string year="", string organizer = "", string place = "",
                                                      string sport = "", string age = "")
        {
            try
            {
                var cups = await _cupRepo.Search(noOfCups,name, year, organizer, place, sport, age);
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
