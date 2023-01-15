using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SportGamesAPI.Data.Interfaces;
using SportGamesAPI.Hubs;
//debug
using System.Diagnostics;

namespace SportGamesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportGameController : ControllerBase
    {
        private readonly IHubContext<SportGameHub> _hub;
        private readonly ISportGameRepository _sportGameRepository;

        public SportGameController(IHubContext<SportGameHub> hub, ISportGameRepository sportGameRepository)
        {
            _hub = hub;
            _sportGameRepository = sportGameRepository;
        }

        [HttpGet("GetSportGames")]
        public async Task<ActionResult<List<SportGame>>> GetSportGames()
        {
            var result = await _sportGameRepository.GetSportGames();
            return Ok(result);
        }

        [HttpGet("GetSportGame/{id}")]
        public async Task<ActionResult<SportGame>> GetSportGame(int id)
        {
            var result = await _sportGameRepository.GetSportGame(id);
            return Ok(result);
        }

        [HttpPost("CreateSportGame")]
        public async Task<ActionResult<SportGame>> CreateSportGame(string team1Name, string team2Name)
        {
            var result = await _sportGameRepository.CreateSportGame(team1Name, team2Name);
            _hub.Clients.All.SendAsync("updategames", await GetSportGames());
            return Ok(result);
        }

        [HttpPut("UpdateSportGame")]
        public async Task<ActionResult<SportGame>> UpdateSportGame(int id, int team1Score, int team2Score)
        {
            var result = await _sportGameRepository.UpdateSportGame(id, team1Score, team2Score);
            Debug.WriteLine(GetSportGames().Result);
            _hub.Clients.All.SendAsync("updategames", await GetSportGames());
            return Ok(result);
        }

        [HttpPut("FinishSportGame")]
        public async Task<ActionResult<SportGame>> FinishSportGame(int id)
        {
            var result = await _sportGameRepository.FinishSportGame(id);
            _hub.Clients.All.SendAsync("updategames", await GetSportGames());
            return Ok(result);
        }

    }
}
