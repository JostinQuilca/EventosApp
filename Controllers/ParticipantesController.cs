using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventosApp.Models;


namespace EventosApp.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ParticipantesController : ControllerBase
{
    private readonly EventDbContext _context;

    public ParticipantesController(EventDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Participante>>> GetParticipantes()
    {
        return await _context.Participantes.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Participante>> GetParticipante(int id)
    {
        var participante = await _context.Participantes.FindAsync(id);
        if (participante == null) return NotFound();
        return participante;
    }

    [HttpPost]
    public async Task<ActionResult<Participante>> CreateParticipante(Participante participante)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        _context.Participantes.Add(participante);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetParticipante), new { id = participante.Id }, participante);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateParticipante(int id, Participante participante)
    {
        if (id != participante.Id) return BadRequest();
        if (!ModelState.IsValid) return BadRequest(ModelState);
        _context.Entry(participante).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ParticipanteExists(id)) return NotFound();
            throw;
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteParticipante(int id)
    {
        var participante = await _context.Participantes.FindAsync(id);
        if (participante == null) return NotFound();
        _context.Participantes.Remove(participante);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool ParticipanteExists(int id)
    {
        return _context.Participantes.Any(e => e.Id == id);
    }
}