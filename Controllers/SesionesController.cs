using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventosApp.Models;


namespace EventosApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SesionesController : ControllerBase
{
    private readonly EventDbContext _context;

    public SesionesController(EventDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Sesion>>> GetSesiones()
    {
        return await _context.Sesiones.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Sesion>> GetSesion(int id)
    {
        var sesion = await _context.Sesiones.FindAsync(id);
        if (sesion == null) return NotFound();
        return sesion;
    }

    [HttpPost]
    public async Task<ActionResult<Sesion>> CreateSesion(Sesion sesion)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        _context.Sesiones.Add(sesion);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetSesion), new { id = sesion.Id }, sesion);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSesion(int id, Sesion sesion)
    {
        if (id != sesion.Id) return BadRequest();
        if (!ModelState.IsValid) return BadRequest(ModelState);
        _context.Entry(sesion).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SesionExists(id)) return NotFound();
            throw;
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSesion(int id)
    {
        var sesion = await _context.Sesiones.FindAsync(id);
        if (sesion == null) return NotFound();
        _context.Sesiones.Remove(sesion);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool SesionExists(int id)
    {
        return _context.Sesiones.Any(e => e.Id == id);
    }
}