// Program.cs
using EventosUTN.EventosUTNConsole.DTOs; // Importa el namespace donde están los DTOs
using System.Net.Http;
using System.Text.Json;

namespace EventosUTNConsole;

class Program
{
    static async Task Main(string[] args)
    {
        // Crear cliente HTTP
        var client = new HttpClient { BaseAddress = new Uri("https://localhost:7238/") };

        try
        {
            // Hacer solicitud GET al endpoint /api/eventos
            var response = await client.GetAsync("api/eventos");

            // Verificar si la solicitud fue exitosa
            if (response.IsSuccessStatusCode)
            {
                // Leer y deserializar la respuesta JSON a una lista de EventoDto
                var jsonString = await response.Content.ReadAsStringAsync();
                var eventos = JsonSerializer.Deserialize<List<EventoDto>>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true // Ignorar mayúsculas/minúsculas en nombres de propiedades
                });

                // Mostrar los eventos en la consola
                Console.WriteLine("Lista de Eventos:");
                Console.WriteLine("-----------------");
                foreach (var evento in eventos)
                {
                    Console.WriteLine($"ID: {evento.Id}");
                    Console.WriteLine($"Nombre: {evento.Nombre}");
                    Console.WriteLine($"Fecha: {evento.Fecha}");
                    Console.WriteLine($"Lugar: {evento.Lugar}");
                    Console.WriteLine($"Tipo de Evento ID: {evento.TipoEventoId}");
                    Console.WriteLine("-----------------");
                }
            }
            else
            {
                Console.WriteLine($"Error al consumir el endpoint: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocurrió un error: {ex.Message}");
        }
    }
}