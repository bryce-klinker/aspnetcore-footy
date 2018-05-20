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
                .Select(p => new PlayerDto
                {
                    FirstName = p.FirstName,
                    Id = p.Id,
                    Surname = p.Surname
                })
                .ToArrayAsync();
            return Ok(players);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var player = await _context.Players
                .Select(p => new PlayerDto
                {
                    FirstName = p.FirstName,
                    Id = p.Id,
                    Surname = p.Surname
                })
                .SingleOrDefaultAsync(p => p.Id == id);
            if (player == null)
                return NotFound();
            return Ok(player);
        }
    }
}