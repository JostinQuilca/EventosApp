using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventosApp.Models;

namespace EventosApp.Controllers;


[Route("api/[controller]")]
[ApiController]
public class EventoPonentesController : ControllerBase
{
    private readonly EventDbContext _context;

    public EventoPonentesController(EventDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EventoPonente>>> GetEventoPonentes()
    {
        return await _context.EventoPonentes.ToListAsync();
    }

    [HttpGet("{eventoId}/{ponenteId}")]
    public async Task<ActionResult<EventoPonente>> GetEventoPonente(int eventoId, int ponenteId)
    {
        var eventoPonente = await _context.EventoPonentes
            .FirstOrDefaultAsync(ep => ep.EventoId == eventoId && ep.PonenteId == ponenteId);
        if (eventoPonente == null) return NotFound();
        return eventoPonente;
    }

    [HttpPost]
    public async Task<ActionResult<EventoPonente>> CreateEventoPonente(EventoPonente eventoPonente)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        _context.EventoPonentes.Add(eventoPonente);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetEventoPonente), new { eventoId = eventoPonente.EventoId, ponenteId = eventoPonente.PonenteId }, eventoPonente);
    }

    [HttpPut("{eventoId}/{ponenteId}")]
    public async Task<IActionResult> UpdateEventoPonente(int eventoId, int ponenteId, EventoPonente eventoPonente)
    {
        if (eventoId != eventoPonente.EventoId || ponenteId != eventoPonente.PonenteId) return BadRequest();
        if (!ModelState.IsValid) return BadRequest(ModelState);
        _context.Entry(eventoPonente).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EventoPonenteExists(eventoId, ponenteId)) return NotFound();
            throw;
        }
        return NoContent();
    }

    [HttpDelete("{eventoId}/{ponenteId}")]
    public async Task<IActionResult> DeleteEventoPonente(int eventoId, int ponenteId)
    {
        var eventoPonente = await _context.EventoPonentes
            .FirstOrDefaultAsync(ep => ep.EventoId == eventoId && ep.PonenteId == ponenteId);
        if (eventoPonente == null) return NotFound();
        _context.EventoPonentes.Remove(eventoPonente);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool EventoPonenteExists(int eventoId, int ponenteId)
    {
        return _context.EventoPonentes.Any(ep => ep.EventoId == eventoId && ep.PonenteId == ponenteId);
    }
}