using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventosApp.Models;

namespace EventosApp.Controllers;


[Route("api/[controller]")]
[ApiController]
public class CertificadosController : ControllerBase
{
    private readonly EventDbContext _context;

    public CertificadosController(EventDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Certificado>>> GetCertificados()
    {
        return await _context.Certificados.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Certificado>> GetCertificado(int id)
    {
        var certificado = await _context.Certificados.FindAsync(id);
        if (certificado == null) return NotFound();
        return certificado;
    }

    [HttpPost]
    public async Task<ActionResult<Certificado>> CreateCertificado(Certificado certificado)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        _context.Certificados.Add(certificado);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetCertificado), new { id = certificado.Id }, certificado);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCertificado(int id, Certificado certificado)
    {
        if (id != certificado.Id) return BadRequest();
        if (!ModelState.IsValid) return BadRequest(ModelState);
        _context.Entry(certificado).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CertificadoExists(id)) return NotFound();
            throw;
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCertificado(int id)
    {
        var certificado = await _context.Certificados.FindAsync(id);
        if (certificado == null) return NotFound();
        _context.Certificados.Remove(certificado);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool CertificadoExists(int id)
    {
        return _context.Certificados.Any(e => e.Id == id);
    }
}