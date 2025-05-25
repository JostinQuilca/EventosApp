using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventosApp.Models;

namespace EventosApp.Controllers;


[Route("api/[controller]")]
[ApiController]
public class EstadosInscripcionController : ControllerBase
{
    private readonly EventDbContext _context;

    public EstadosInscripcionController(EventDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EstadoInscripcion>>> GetEstadosInscripcion()
    {
        return await _context.EstadosInscripcion.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EstadoInscripcion>> GetEstadoInscripcion(int id)
    {
        var estado = await _context.EstadosInscripcion.FindAsync(id);
        if (estado == null) return NotFound();
        return estado;
    }

    [HttpPost]
    public async Task<ActionResult<EstadoInscripcion>> CreateEstadoInscripcion(EstadoInscripcion estado)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        _context.EstadosInscripcion.Add(estado);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetEstadoInscripcion), new { id = estado.Id }, estado);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEstadoInscripcion(int id, EstadoInscripcion estado)
    {
        if (id != estado.Id) return BadRequest();
        if (!ModelState.IsValid) return BadRequest(ModelState);
        _context.Entry(estado).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EstadoInscripcionExists(id)) return NotFound();
            throw;
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEstadoInscripcion(int id)
    {
        var estado = await _context.EstadosInscripcion.FindAsync(id);
        if (estado == null) return NotFound();
        _context.EstadosInscripcion.Remove(estado);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool EstadoInscripcionExists(int id)
    {
        return _context.EstadosInscripcion.Any(e => e.Id == id);
    }
}