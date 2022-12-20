using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Backend.Services;
using Backend.Models;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {
        public ISquareService _squareService;
        public PlayersController(ISquareService squareService) 
        {
            _squareService = squareService;
        }

        // GET api/players
        [HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    List<string> players = _squareService.GetPlayers();
        //    return players;
        //}
        public IActionResult Get()
        {
            List<PlayerViewModel> players = _squareService.GetPlayers();
            return Ok(players);
        }
    }
}
