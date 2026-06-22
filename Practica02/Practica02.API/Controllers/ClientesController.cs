using Microsoft.AspNetCore.Mvc;
using Practica02.API.Models;
using Practica02.API.Services;

namespace Practica02.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly ILogger<ClientesController> _logger;

        public ClientesController(IClienteService clienteService, ILogger<ClientesController> logger)
        {
            _clienteService = clienteService;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todos los clientes registrados.
        /// </summary>
        /// <returns>Lista de clientes</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> ObtenerTodos()
        {
            try
            {
                var clientes = await _clienteService.ObtenerTodosAsync();
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener clientes");
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al obtener clientes" });
            }
        }

        /// <summary>
        /// Obtiene un cliente por su ID.
        /// </summary>
        /// <param name="id">ID del cliente</param>
        /// <returns>Datos del cliente</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> ObtenerPorId(long id)
        {
            try
            {
                var cliente = await _clienteService.ObtenerPorIdAsync(id);
                if (cliente == null)
                    return NotFound(new { mensaje = $"No se encontró cliente con ID {id}" });

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener cliente por ID");
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al obtener cliente" });
            }
        }

        /// <summary>
        /// Obtiene un cliente por su cédula.
        /// </summary>
        /// <param name="cedula">Cédula del cliente</param>
        /// <returns>Datos del cliente</returns>
        [HttpGet("cedula/{cedula}")]
        public async Task<ActionResult<Cliente>> ObtenerPorCedula(string cedula)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cedula))
                    return BadRequest(new { mensaje = "La cédula es requerida" });

                var cliente = await _clienteService.ObtenerPorCedulaAsync(cedula);
                if (cliente == null)
                    return NotFound(new { mensaje = $"No se encontró cliente con cédula {cedula}" });

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener cliente por cédula");
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al obtener cliente" });
            }
        }

        /// <summary>
        /// Crea un nuevo cliente.
        /// </summary>
        /// <param name="cliente">Datos del cliente a crear</param>
        /// <returns>ID del cliente creado</returns>
        [HttpPost]
        public async Task<ActionResult<long>> Crear([FromBody] Cliente cliente)
        {
            try
            {
                if (cliente == null)
                    return BadRequest(new { mensaje = "Los datos del cliente no pueden estar vacíos" });

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var id = await _clienteService.CrearAsync(cliente);
                return CreatedAtAction(nameof(ObtenerPorId), new { id }, id);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Error de validación al crear cliente");
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear cliente");
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al crear cliente" });
            }
        }

        /// <summary>
        /// Actualiza un cliente existente.
        /// </summary>
        /// <param name="id">ID del cliente</param>
        /// <param name="cliente">Datos actualizados del cliente</param>
        /// <returns>Resultado de la operación</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<object>> Actualizar(long id, [FromBody] Cliente cliente)
        {
            try
            {
                if (cliente == null || cliente.IdCliente != id)
                    return BadRequest(new { mensaje = "Los datos del cliente no coinciden" });

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var resultado = await _clienteService.ActualizarAsync(cliente);
                if (!resultado)
                    return NotFound(new { mensaje = $"No se encontró cliente con ID {id}" });

                return Ok(new { mensaje = "Cliente actualizado exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar cliente");
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al actualizar cliente" });
            }
        }

        /// <summary>
        /// Elimina un cliente.
        /// </summary>
        /// <param name="id">ID del cliente a eliminar</param>
        /// <returns>Resultado de la operación</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<object>> Eliminar(long id)
        {
            try
            {
                var resultado = await _clienteService.EliminarAsync(id);
                if (!resultado)
                    return NotFound(new { mensaje = $"No se encontró cliente con ID {id}" });

                return Ok(new { mensaje = "Cliente eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar cliente");
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al eliminar cliente" });
            }
        }
    }
}
