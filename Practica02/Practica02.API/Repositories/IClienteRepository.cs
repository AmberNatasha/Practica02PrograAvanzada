using Practica02.API.Models;

namespace Practica02.API.Repositories
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> ObtenerTodos();
        Task<Cliente?> ObtenerPorId(long idCliente);
        Task<Cliente?> ObtenerPorCedula(string cedula);
        Task<long> Crear(Cliente cliente);
        Task<bool> Actualizar(Cliente cliente);
        Task<bool> Eliminar(long idCliente);
    }
}
