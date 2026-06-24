using Microsoft.AspNetCore.Mvc;
using Practica02.Models;
using System.Net.Http.Json;

namespace Practica02.Controllers
{
    public class MascotasController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public MascotasController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        // Método para obtener lista de clientes desde la API
        private async Task<List<ClienteDto>> ObtenerClientesDesdeAPI()
        {
            try
            {
                var apiUrl = _configuration["ApiSettings:BaseUrl"] ?? "http://localhost:5000";
                var endpoint = $"{apiUrl}/api/clientes";

                using (var response = await _httpClient.GetAsync(endpoint))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var clientes = await response.Content.ReadFromJsonAsync<List<ClienteDto>>();
                        return clientes ?? new List<ClienteDto>();
                    }
                }
            }
            catch (Exception)
            {
                // Si hay error, retornar lista vacía
            }

            return new List<ClienteDto>();
        }

        // DTO para cliente desde API
        public class ClienteDto
        {
            public long IdCliente { get; set; }
            public string? Cedula { get; set; }
            public string? Nombre { get; set; }
            public string? Correo { get; set; }
            public bool Estado { get; set; }
        }

        // GET: Mascotas/Registrar
        public async Task<IActionResult> Registrar()
        {
            var clientes = await ObtenerClientesDesdeAPI();
            ViewBag.Clientes = clientes;
            return View(new Mascota());
        }

        // POST: Mascotas/Registrar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrar(Mascota mascota)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var apiUrl = _configuration["ApiSettings:BaseUrl"] ?? "http://localhost:5000";
                    var endpoint = $"{apiUrl}/api/mascotas";

                    // Crear objeto DTO para enviar a la API
                    var mascotaDto = new
                    {
                        mascota.Nombre,
                        mascota.Especie,
                        mascota.Raza,
                        mascota.Peso,
                        IdCliente = mascota.ClienteId
                    };

                    using (var response = await _httpClient.PostAsJsonAsync(endpoint, mascotaDto))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            TempData["Mensaje"] = $"Mascota {mascota.Nombre} registrada exitosamente.";
                            TempData["Tipo"] = "success";
                            return RedirectToAction(nameof(Registrar));
                        }
                        else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                        {
                            var errorContent = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                            TempData["Mensaje"] = errorContent != null && errorContent.TryGetValue("mensaje", out var mensaje)
                                ? mensaje
                                : "Error en la validación de la mascota.";
                            TempData["Tipo"] = "danger";
                        }
                        else
                        {
                            TempData["Mensaje"] = "Error al registrar la mascota. Por favor, intenta nuevamente.";
                            TempData["Tipo"] = "danger";
                        }
                    }
                }
                catch (Exception ex)
                {
                    TempData["Mensaje"] = $"Error de conexión: {ex.Message}";
                    TempData["Tipo"] = "danger";
                }
            }

            // Mostrar formulario nuevamente con clientes
            var clientesList = await ObtenerClientesDesdeAPI();
            ViewBag.Clientes = clientesList;
            return View(mascota);
        }

        // GET: Mascotas/Consulta
        public async Task<IActionResult> Consulta()
        {
            try
            {
                var apiUrl = _configuration["ApiSettings:BaseUrl"] ?? "http://localhost:5120";
                var endpoint = $"{apiUrl}/api/mascotas/consulta";

                var mascotas = await _httpClient.GetFromJsonAsync<List<MascotaConsulta>>(endpoint);

                return View(mascotas ?? new List<MascotaConsulta>());
            }
            catch
            {
                return View(new List<MascotaConsulta>());
            }
        }
    }
}
