using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventosApp.Models;


namespace EventosApp.Controllers;


[Route("api/[controller]")]
[ApiController]
public class SalasController : ControllerBase
{
    private readonly EventDbContext _context;

    public SalasController(EventDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Sala>>> GetSalas()
    {
        return await _context.Salas.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Sala>> GetSala(int id)
    {
        var sala = await _context.Salas.FindAsync(id);
        if (sala == null) return NotFound();
        return sala;
    }

    [HttpPost]
    public async Task<ActionResult<Sala>> CreateSala(Sala sala)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        _context.Salas.Add(sala);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetSala), new { id = sala.Id }, sala);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSala(int id, Sala sala)
    {
        if (id != sala.Id) return BadRequest();
        if (!ModelState.IsValid) return BadRequest(ModelState);
        _context.Entry(sala).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SalaExists(id)) return NotFound();
            throw;
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSala(int id)
    {
        var sala = await _context.Salas.FindAsync(id);
        if (sala == null) return NotFound();
        _context.Salas.Remove(sala);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool SalaExists(int id)
    {
        return _context.Salas.Any(e => e.Id == id);
    }
}