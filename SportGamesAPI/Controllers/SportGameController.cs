using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportGamesAPI.Data.Interfaces;

namespace SportGamesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportGameController : ControllerBase
    {
        private readonly ISportGameRepository _sportGameRepository;

        public SportGameController(ISportGameRepository sportGameRepository)
        {
            _sportGameRepository = sportGameRepository;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public async Task<ActionResult<List<SportGame>>> GetSportGames()
        {
            var result = await _sportGameRepository.GetSportGames();
            return Ok(result);
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public async Task<ActionResult<SportGame>> GetSportGame(int id)
        {
            var result = await _sportGameRepository.GetSportGame(id);
            return Ok(result);
        }

        [HttpPost]
        [Route("api/[controller]")]
        public async Task<ActionResult<SportGame>> CreateSportGame(SportGame sportGame)
        {
            var result = await _sportGameRepository.CreateSportGame(sportGame);
            return Ok(result);
        }

        [HttpPut]
        [Route("api/[controller]")]
        public async Task<ActionResult<SportGame>> UpdateSportGame(SportGame sportGame)
        {
            var result = await _sportGameRepository.UpdateSportGame(sportGame);
            return Ok(result);
        }
    }
}
