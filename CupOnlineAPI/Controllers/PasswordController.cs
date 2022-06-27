using CupOnlineAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CupOnlineAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class PasswordController : ControllerBase
    {
        private readonly PasswordRepository _repo;

        public PasswordController(PasswordRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<string> CreatePassword()
        {
            try
            {
                var password = await _repo.RandomPassword();
                return password;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public async Task<string> HashPassword(string password, int iterations = 100000)
        {
            try
            {
                var hash = await _repo.HashPassword(password, iterations);
                return hash;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
