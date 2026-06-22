using Practica02.API.Models;

namespace Practica02.API.Services
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> ObtenerTodosAsync();
        Task<Cliente?> ObtenerPorIdAsync(long idCliente);
        Task<Cliente?> ObtenerPorCedulaAsync(string cedula);
        Task<long> CrearAsync(Cliente cliente);
        Task<bool> ActualizarAsync(Cliente cliente);
        Task<bool> EliminarAsync(long idCliente);
    }
}
