﻿using CupOnlineAPI.Models;
using CupOnlineAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CupOnlineAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class SportController : ControllerBase
    {
        private readonly ISportRepository _sportRepo;

        public SportController(ISportRepository sportRepo)
        {
            _sportRepo = sportRepo;
        }

        [HttpGet]
        public IEnumerable<Sport> GetAll()
        {
            try
            {
                return _sportRepo.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
