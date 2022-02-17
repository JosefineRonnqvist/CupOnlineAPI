using CupOnlineAPI.Models;
using CupOnlineAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CupOnlineAPI.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderRepository _orderRepo;

        public OrderController(OrderRepository organizer)
        {
            _orderRepo =organizer;
        }

        /// <summary>
        /// Get all sports from cuponline with GetAll-method (no query)
        /// </summary>
        /// <returns>List of sports</returns>
        /// <exception cref="Exception"></exception>
        [HttpGet]
        public IEnumerable<Organizer> GetAllOrganizers()
        {
            try
            {
                return _orderRepo.GetAllOrganizers();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get all sports from cuponline with GetAll-method (no query)
        /// </summary>
        /// <returns>List of sports</returns>
        /// <exception cref="Exception"></exception>
        [HttpGet]
        public IEnumerable<Sport> GetAllSports()
        {
            try
            {
                return _orderRepo.GetAllSports();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateCup(OrderCup cup)
        //{
        //    try 
        //    {
        //        var createdCup = await _orderRepo.CreateCup(cup);
        //        return CreatedAtRoute("CupById", new { id = createdCup.id }, createdCup);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}
    }
}
