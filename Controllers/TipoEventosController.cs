using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventosApp.Models;


namespace EventosApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TipoEventosController : ControllerBase
{
    private readonly EventDbContext _context;

    public TipoEventosController(EventDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TipoEvento>>> GetTipoEventos()
    {
        return await _context.TipoEventos.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TipoEvento>> GetTipoEvento(int id)
    {
        var tipo = await _context.TipoEventos.FindAsync(id);
        if (tipo == null) return NotFound();
        return tipo;
    }

    [HttpPost]
    public async Task<ActionResult<TipoEvento>> CreateTipoEvento(TipoEvento tipo)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        _context.TipoEventos.Add(tipo);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetTipoEvento), new { id = tipo.Id }, tipo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTipoEvento(int id, TipoEvento tipo)
    {
        if (id != tipo.Id) return BadRequest();
        if (!ModelState.IsValid) return BadRequest(ModelState);
        _context.Entry(tipo).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TipoEventoExists(id)) return NotFound();
            throw;
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTipoEvento(int id)
    {
        var tipo = await _context.TipoEventos.FindAsync(id);
        if (tipo == null) return NotFound();
        _context.TipoEventos.Remove(tipo);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool TipoEventoExists(int id)
    {
        return _context.TipoEventos.Any(e => e.Id == id);
    }
}