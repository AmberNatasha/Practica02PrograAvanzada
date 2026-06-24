using Microsoft.AspNetCore.Mvc;
using Practica02.API.Models;
using Practica02.API.Services;

namespace Practica02.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MascotasController : ControllerBase
    {
        private readonly IMascotaService _mascotaService;
        private readonly ILogger<MascotasController> _logger;

        public MascotasController(IMascotaService mascotaService, ILogger<MascotasController> logger)
        {
            _mascotaService = mascotaService;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todas las mascotas registradas.
        /// </summary>
        /// <returns>Lista de mascotas</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mascota>>> ObtenerTodas()
        {
            try
            {
                var mascotas = await _mascotaService.ObtenerTodasAsync();
                return Ok(mascotas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener mascotas");
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al obtener mascotas" });
            }
        }

        /// <summary>
        /// Obtiene una mascota por su ID.
        /// </summary>
        /// <param name="id">ID de la mascota</param>
        /// <returns>Datos de la mascota</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Mascota>> ObtenerPorId(long id)
        {
            try
            {
                var mascota = await _mascotaService.ObtenerPorIdAsync(id);
                if (mascota == null)
                    return NotFound(new { mensaje = $"No se encontró mascota con ID {id}" });

                return Ok(mascota);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener mascota por ID");
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al obtener mascota" });
            }
        }

        /// <summary>
        /// Obtiene todas las mascotas de un cliente específico.
        /// </summary>
        /// <param name="idCliente">ID del cliente</param>
        /// <returns>Lista de mascotas del cliente</returns>
        [HttpGet("cliente/{idCliente}")]
        public async Task<ActionResult<IEnumerable<Mascota>>> ObtenerPorIdCliente(long idCliente)
        {
            try
            {
                var mascotas = await _mascotaService.ObtenerPorIdClienteAsync(idCliente);
                return Ok(mascotas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener mascotas por cliente");
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al obtener mascotas" });
            }
        }

        /// <summary>
        /// Crea una nueva mascota.
        /// </summary>
        /// <param name="mascota">Datos de la mascota a crear</param>
        /// <returns>ID de la mascota creada</returns>
        [HttpPost]
        public async Task<ActionResult<long>> Crear([FromBody] Mascota mascota)
        {
            try
            {
                if (mascota == null)
                    return BadRequest(new { mensaje = "Los datos de la mascota no pueden estar vacíos" });

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var id = await _mascotaService.CrearAsync(mascota);
                return CreatedAtAction(nameof(ObtenerPorId), new { id }, id);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Error de validación al crear mascota");
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear mascota");
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al crear mascota" });
            }
        }

        /// <summary>
        /// Actualiza una mascota existente.
        /// </summary>
        /// <param name="id">ID de la mascota</param>
        /// <param name="mascota">Datos actualizados de la mascota</param>
        /// <returns>Resultado de la operación</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<object>> Actualizar(long id, [FromBody] Mascota mascota)
        {
            try
            {
                if (mascota == null || mascota.IdMascota != id)
                    return BadRequest(new { mensaje = "Los datos de la mascota no coinciden" });

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var resultado = await _mascotaService.ActualizarAsync(mascota);
                if (!resultado)
                    return NotFound(new { mensaje = $"No se encontró mascota con ID {id}" });

                return Ok(new { mensaje = "Mascota actualizada exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar mascota");
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al actualizar mascota" });
            }
        }

        /// <summary>
        /// Elimina una mascota.
        /// </summary>
        /// <param name="id">ID de la mascota a eliminar</param>
        /// <returns>Resultado de la operación</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<object>> Eliminar(long id)
        {
            try
            {
                var resultado = await _mascotaService.EliminarAsync(id);
                if (!resultado)
                    return NotFound(new { mensaje = $"No se encontró mascota con ID {id}" });

                return Ok(new { mensaje = "Mascota eliminada exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar mascota");
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al eliminar mascota" });
            }
        }

        [HttpGet("consulta")]
        public async Task<ActionResult<IEnumerable<MascotaConsulta>>> ConsultarMascotas()
        {
            try
            {
                var mascotas = await _mascotaService.ConsultarMascotasAsync();
                return Ok(mascotas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al consultar mascotas");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { mensaje = "Error al consultar mascotas" });
            }
        }
    }
}

