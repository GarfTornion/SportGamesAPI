using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SportGamesAPI.Data.Interfaces;
using SportGamesAPI.Hubs;
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
            try
            {
                var result = await _sportGameRepository.GetSportGames();
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("GetSportGame/{id}")]
        public async Task<ActionResult<SportGame>> GetSportGame(int id)
        {
            try
            {
                var result = await _sportGameRepository.GetSportGame(id);
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("CreateSportGame")]
        public async Task<ActionResult<SportGame>> CreateSportGame(string team1Name, string team2Name)
        {
            try
            {
                var result = await _sportGameRepository.CreateSportGame(team1Name, team2Name);
                var games = await GetSportGames();
                await _hub.Clients.All.SendAsync("updategames", games.Value);
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("UpdateSportGame")]
        public async Task<ActionResult<SportGame>> UpdateSportGame(int id, int team1Score, int team2Score)
        {
            try
            {
                var result = await _sportGameRepository.UpdateSportGame(id, team1Score, team2Score);
                var games = await GetSportGames();
                await _hub.Clients.All.SendAsync("updategames", games.Value);
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("FinishSportGame")]
        public async Task<ActionResult<SportGame>> FinishSportGame(int id)
        {
            try
            {
                var result = await _sportGameRepository.FinishSportGame(id);
                var games = await GetSportGames();
                _hub.Clients.All.SendAsync("updategames", games.Value);
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

    }
}
