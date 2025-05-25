using Microsoft.EntityFrameworkCore;

namespace EventosApp.Models;

public class EventDbContext : DbContext
{
    public EventDbContext(DbContextOptions<EventDbContext> options) : base(options) { }

    // DbSets para todas las entidades
    public DbSet<Certificado> Certificados { get; set; }
    public DbSet<EstadoInscripcion> EstadosInscripcion { get; set; }
    public DbSet<Evento> Eventos { get; set; }
    public DbSet<EventoPonente> EventoPonentes { get; set; }
    public DbSet<Inscripcion> Inscripciones { get; set; }
    public DbSet<MetodoPago> MetodosPago { get; set; }
    public DbSet<Pago> Pagos { get; set; }
    public DbSet<Participante> Participantes { get; set; }
    public DbSet<Ponente> Ponentes { get; set; }
    public DbSet<Sala> Salas { get; set; }
    public DbSet<Sesion> Sesiones { get; set; }
    public DbSet<TipoEvento> TipoEventos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuración de claves primarias y relaciones

        // Certificado
        modelBuilder.Entity<Certificado>()
            .HasKey(c => c.Id);
        modelBuilder.Entity<Certificado>()
            .HasOne(c => c.Inscripcion)
            .WithOne(i => i.Certificado)
            .HasForeignKey<Certificado>(c => c.InscripcionId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Certificado>()
            .HasIndex(c => c.InscripcionId)
            .IsUnique();

        // EstadoInscripcion
        modelBuilder.Entity<EstadoInscripcion>()
            .HasKey(ei => ei.Id);
        modelBuilder.Entity<EstadoInscripcion>()
            .HasMany(ei => ei.Inscripciones)
            .WithOne(i => i.Estado)
            .HasForeignKey(i => i.EstadoId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<EstadoInscripcion>()
            .HasIndex(ei => ei.Nombre)
            .IsUnique();

        // Evento
        modelBuilder.Entity<Evento>()
            .HasKey(e => e.Id);
        modelBuilder.Entity<Evento>()
            .HasOne(e => e.TipoEvento)
            .WithMany(te => te.Eventos)
            .HasForeignKey(e => e.TipoEventoId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Evento>()
            .HasMany(e => e.Inscripciones)
            .WithOne(i => i.Evento)
            .HasForeignKey(i => i.EventoId)
            .OnDelete(DeleteBehavior.Cascade); // Cambiado a Cascade
        modelBuilder.Entity<Evento>()
            .HasMany(e => e.Sesiones)
            .WithOne(s => s.Evento)
            .HasForeignKey(s => s.EventoId)
            .OnDelete(DeleteBehavior.Cascade); // Cambiado a Cascade
        modelBuilder.Entity<Evento>()
            .HasMany(e => e.EventoPonentes)
            .WithOne(ep => ep.Evento)
            .HasForeignKey(ep => ep.EventoId)
            .OnDelete(DeleteBehavior.Cascade); // Cambiado a Cascade
        modelBuilder.Entity<Evento>()
            .HasIndex(e => e.Nombre);

        // EventoPonente (tabla intermedia para relación muchos a muchos)
        modelBuilder.Entity<EventoPonente>()
            .HasKey(ep => new { ep.EventoId, ep.PonenteId });
        modelBuilder.Entity<EventoPonente>()
            .HasOne(ep => ep.Evento)
            .WithMany(e => e.EventoPonentes)
            .HasForeignKey(ep => ep.EventoId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<EventoPonente>()
            .HasOne(ep => ep.Ponente)
            .WithMany(p => p.EventoPonentes)
            .HasForeignKey(ep => ep.PonenteId)
            .OnDelete(DeleteBehavior.Restrict);

        // Inscripcion
        modelBuilder.Entity<Inscripcion>()
            .HasKey(i => i.Id);
        modelBuilder.Entity<Inscripcion>()
            .HasOne(i => i.Evento)
            .WithMany(e => e.Inscripciones)
            .HasForeignKey(i => i.EventoId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Inscripcion>()
            .HasOne(i => i.Participante)
            .WithMany(p => p.Inscripciones)
            .HasForeignKey(i => i.ParticipanteId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Inscripcion>()
            .HasOne(i => i.Estado)
            .WithMany(ei => ei.Inscripciones)
            .HasForeignKey(i => i.EstadoId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Inscripcion>()
            .HasOne(i => i.Pago)
            .WithOne(p => p.Inscripcion)
            .HasForeignKey<Pago>(p => p.InscripcionId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Inscripcion>()
            .HasOne(i => i.Certificado)
            .WithOne(c => c.Inscripcion)
            .HasForeignKey<Certificado>(c => c.InscripcionId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Inscripcion>()
            .HasIndex(i => i.EventoId);
        modelBuilder.Entity<Inscripcion>()
            .HasIndex(i => i.ParticipanteId);

        // MetodoPago
        modelBuilder.Entity<MetodoPago>()
            .HasKey(mp => mp.Id);
        modelBuilder.Entity<MetodoPago>()
            .HasMany(mp => mp.Pagos)
            .WithOne(p => p.MetodoPago)
            .HasForeignKey(p => p.MetodoPagoId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<MetodoPago>()
            .HasIndex(mp => mp.Nombre)
            .IsUnique();

        // Pago
        modelBuilder.Entity<Pago>()
            .HasKey(p => p.Id);
        modelBuilder.Entity<Pago>()
            .HasOne(p => p.Inscripcion)
            .WithOne(i => i.Pago)
            .HasForeignKey<Pago>(p => p.InscripcionId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Pago>()
            .HasOne(p => p.MetodoPago)
            .WithMany(mp => mp.Pagos)
            .HasForeignKey(p => p.MetodoPagoId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Pago>()
            .HasIndex(p => p.InscripcionId)
            .IsUnique();

        // Participante
        modelBuilder.Entity<Participante>()
            .HasKey(p => p.Id);
        modelBuilder.Entity<Participante>()
            .HasMany(p => p.Inscripciones)
            .WithOne(i => i.Participante)
            .HasForeignKey(i => i.ParticipanteId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Participante>()
            .HasIndex(p => p.Email)
            .IsUnique();

        // Ponente
        modelBuilder.Entity<Ponente>()
            .HasKey(p => p.Id);
        modelBuilder.Entity<Ponente>()
            .HasMany(p => p.EventoPonentes)
            .WithOne(ep => ep.Ponente)
            .HasForeignKey(ep => ep.PonenteId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Ponente>()
            .HasIndex(p => p.Email)
            .IsUnique();

        // Sala
        modelBuilder.Entity<Sala>()
            .HasKey(s => s.Id);
        modelBuilder.Entity<Sala>()
            .HasIndex(s => s.Nombre)
            .IsUnique();

        // Sesion
        modelBuilder.Entity<Sesion>()
            .HasKey(s => s.Id);
        modelBuilder.Entity<Sesion>()
            .HasOne(s => s.Evento)
            .WithMany(e => e.Sesiones)
            .HasForeignKey(s => s.EventoId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Sesion>()
            .HasOne(s => s.Sala)
            .WithMany()
            .HasForeignKey(s => s.SalaId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Sesion>()
            .HasIndex(s => s.EventoId);

        // TipoEvento
        modelBuilder.Entity<TipoEvento>()
            .HasKey(te => te.Id);
        modelBuilder.Entity<TipoEvento>()
            .HasMany(te => te.Eventos)
            .WithOne(e => e.TipoEvento)
            .HasForeignKey(e => e.TipoEventoId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<TipoEvento>()
            .HasIndex(te => te.Nombre)
            .IsUnique();
    }
}