using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventosApp.Models;


namespace EventosApp.Controllers;


[Route("api/[controller]")]
[ApiController]
public class InscripcionesController : ControllerBase
{
    private readonly EventDbContext _context;

    public InscripcionesController(EventDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Inscripcion>>> GetInscripciones()
    {
        return await _context.Inscripciones.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Inscripcion>> GetInscripcion(int id)
    {
        var inscripcion = await _context.Inscripciones.FindAsync(id);
        if (inscripcion == null) return NotFound();
        return inscripcion;
    }

    [HttpPost]
    public async Task<ActionResult<Inscripcion>> CreateInscripcion(Inscripcion inscripcion)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        _context.Inscripciones.Add(inscripcion);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetInscripcion), new { id = inscripcion.Id }, inscripcion);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateInscripcion(int id, Inscripcion inscripcion)
    {
        if (id != inscripcion.Id) return BadRequest();
        if (!ModelState.IsValid) return BadRequest(ModelState);
        _context.Entry(inscripcion).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!InscripcionExists(id)) return NotFound();
            throw;
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInscripcion(int id)
    {
        var inscripcion = await _context.Inscripciones.FindAsync(id);
        if (inscripcion == null) return NotFound();
        _context.Inscripciones.Remove(inscripcion);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool InscripcionExists(int id)
    {
        return _context.Inscripciones.Any(e => e.Id == id);
    }
}