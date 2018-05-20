using System.Linq;
using System.Threading.Tasks;
using Footy.Models.Players;
using Footy.Rest.Api.General;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Footy.Rest.Api.Players
{
    [Route("players")]
    public class PlayersController : Controller
    {
        private readonly FootyContext _context;

        public PlayersController(FootyContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var players = await _context.Players
                .Select(p => new PlayerDto())
                .ToArrayAsync();
            return Ok(players);
        }
    }
}