using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventosApp.Models;


namespace EventosApp.Controllers;


[Route("api/[controller]")]
[ApiController]
public class MetodosPagoController : ControllerBase
{
    private readonly EventDbContext _context;

    public MetodosPagoController(EventDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MetodoPago>>> GetMetodosPago()
    {
        return await _context.MetodosPago.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MetodoPago>> GetMetodoPago(int id)
    {
        var metodo = await _context.MetodosPago.FindAsync(id);
        if (metodo == null) return NotFound();
        return metodo;
    }

    [HttpPost]
    public async Task<ActionResult<MetodoPago>> CreateMetodoPago(MetodoPago metodo)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        _context.MetodosPago.Add(metodo);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetMetodoPago), new { id = metodo.Id }, metodo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMetodoPago(int id, MetodoPago metodo)
    {
        if (id != metodo.Id) return BadRequest();
        if (!ModelState.IsValid) return BadRequest(ModelState);
        _context.Entry(metodo).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!MetodoPagoExists(id)) return NotFound();
            throw;
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMetodoPago(int id)
    {
        var metodo = await _context.MetodosPago.FindAsync(id);
        if (metodo == null) return NotFound();
        _context.MetodosPago.Remove(metodo);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool MetodoPagoExists(int id)
    {
        return _context.MetodosPago.Any(e => e.Id == id);
    }
}