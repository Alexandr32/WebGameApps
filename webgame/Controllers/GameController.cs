using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationGame.Dal;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication.Controllers
{
    [Route("[controller]")]
    public class GameController : Controller
    {

        private readonly IGameRepository _gameRepository;
        private Guid UserId => Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

        public GameController()
        {
            _gameRepository = new GameRepository();
        }
        
        [Route("GetAllGame")]
        [HttpGet]
        public IActionResult GetAllGame()
        {
            var allGames = _gameRepository.GetAll();
            return Ok(allGames);
        }

        [Route("CurrentGame")]
        [Authorize (Roles = "user")]
        [HttpGet]
        public IActionResult CurrentGame()
        {
            if(!_gameRepository.GetCurrentGame().ContainsKey(UserId))
            {
                var game = Enumerable.Empty<Game>();
                return Ok(game);
            }

            var gameId = _gameRepository.GetCurrentGame().Single(s => s.Key == UserId).Value;
            var currentGame = _gameRepository.GetAll().Where(w => w.Id == gameId).FirstOrDefault();

            return Ok(currentGame);
        }

    }
}
