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

        [HttpGet("{id}")]
        public async Task<ActionResult<Organizer>> GetOrganizerById(int id)
        {
            try
            {
                var organizer =await _orderRepo.GetOrganizerById(id);
                return Ok(organizer);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get all organizers from cuponline ordered alphabetically
        /// </summary>
        /// <returns>List of organizers</returns>
        /// <exception cref="Exception"></exception>
        [HttpGet]
        public async Task<IActionResult> GetAllOrganizers()
        {
            try
            {
                 var organizers = await _orderRepo.GetAllOrganizers();
                return Ok(organizers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
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

        [HttpPost]
        public async Task<ActionResult<City>> CreateCity(City city)
        {
            try
            {
                city.city_id = await _orderRepo.CreateCity(city);
                return Ok(city);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Organizer>> CreateOrganizer(Organizer organizer)
        {
            try
            {
                organizer.club_id =await _orderRepo.CreateOrganizer(organizer);
                return Ok(organizer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCup(OrderCup cup)
        {
            try
            {
                cup.cup_id = await _orderRepo.CreateCup(cup);
                return Ok(cup);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<OrderRegistration>> CreateCupRegistration(OrderRegistration reg)
        {
            try
            {
                reg.id = await _orderRepo.CreateCupRegistration(reg);
                return Ok(reg);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCupAdmin(OrderCupAdmin admin)
        {
            try
            {
                admin.cup_user_id = await _orderRepo.CreateCupAdmin(admin);
                return Ok(admin);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SendConfirmationMailSe(ConfirmationMailDetails confirmation)
        {
            try
            {
                await _orderRepo.SendConfirmationMailSe(confirmation);
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SendConfirmationMailEn(ConfirmationMailDetails confirmation)
        {
            try
            {
                await _orderRepo.SendConfirmationMailEn(confirmation);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SendOrderMail(OrderMailDetails orderDetails)
        {
            try
            {
                await _orderRepo.SendOrderMail(orderDetails);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
