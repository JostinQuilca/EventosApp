using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventosApp.Models;


namespace EventosApp.Controllers;


[Route("api/[controller]")]
[ApiController]
public class PonentesController : ControllerBase
{
    private readonly EventDbContext _context;

    public PonentesController(EventDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Ponente>>> GetPonentes()
    {
        return await _context.Ponentes.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Ponente>> GetPonente(int id)
    {
        var ponente = await _context.Ponentes.FindAsync(id);
        if (ponente == null) return NotFound();
        return ponente;
    }

    [HttpPost]
    public async Task<ActionResult<Ponente>> CreatePonente(Ponente ponente)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        _context.Ponentes.Add(ponente);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPonente), new { id = ponente.Id }, ponente);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePonente(int id, Ponente ponente)
    {
        if (id != ponente.Id) return BadRequest();
        if (!ModelState.IsValid) return BadRequest(ModelState);
        _context.Entry(ponente).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PonenteExists(id)) return NotFound();
            throw;
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePonente(int id)
    {
        var ponente = await _context.Ponentes.FindAsync(id);
        if (ponente == null) return NotFound();
        _context.Ponentes.Remove(ponente);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool PonenteExists(int id)
    {
        return _context.Ponentes.Any(e => e.Id == id);
    }
}