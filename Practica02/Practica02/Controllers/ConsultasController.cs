using Microsoft.AspNetCore.Mvc;
using Practica02.Models;

namespace Practica02.Controllers
{
    public class ConsultasController : Controller
    {
        // Clase interna para la vista de consultas de mascotas
        public class MascotaConsultaDto
        {
            public string? CedulaCliente { get; set; }
            public string? NombreCliente { get; set; }
            public string? NombreMascota { get; set; }
            public string? Especie { get; set; }
            public decimal Peso { get; set; }
        }

        // Método privado para obtener mascotas simuladas
        private List<MascotaConsultaDto> ObtenerMascotasSimuladas()
        {
            return new List<MascotaConsultaDto>
            {
                new MascotaConsultaDto { CedulaCliente = "123456789", NombreCliente = "Juan García", NombreMascota = "Fido", Especie = "Perro", Peso = 25.5m },
                new MascotaConsultaDto { CedulaCliente = "123456789", NombreCliente = "Juan García", NombreMascota = "Misu", Especie = "Gato", Peso = 4.2m },
                new MascotaConsultaDto { CedulaCliente = "987654321", NombreCliente = "María López", NombreMascota = "Rex", Especie = "Perro", Peso = 35.0m },
                new MascotaConsultaDto { CedulaCliente = "456789123", NombreCliente = "Carlos Martínez", NombreMascota = "Tweety", Especie = "Pájaro", Peso = 0.5m },
                new MascotaConsultaDto { CedulaCliente = "789123456", NombreCliente = "Ana Rodríguez", NombreMascota = "Nemo", Especie = "Pez", Peso = 0.1m }
            };
        }

        // GET: Consultas/Index
        public IActionResult Index()
        {
            ViewBag.Mensaje = "Módulo de Consultas";
            return View();
        }

        // GET: Consultas/Mascotas
        public IActionResult Mascotas()
        {
            var mascotas = ObtenerMascotasSimuladas();
            return View(mascotas);
        }
    }
}
