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
    public class SquaresController : ControllerBase
    {
        public ISquareService _squareService;
        public SquaresController(ISquareService squareService) 
        {
            _squareService = squareService;
        }

        // GET api/squares
        [HttpGet]
        public IActionResult Get()
        {
            CardViewModel squares = _squareService.GetSquares();
            return Ok(squares);
        }

        // GET api/squares/martinez
        [HttpGet("{player}")]
        public IActionResult Get(string player)
        {
            CardViewModel squares = _squareService.GetSquares(player);
            return Ok(squares);
        }
        
    }
}
