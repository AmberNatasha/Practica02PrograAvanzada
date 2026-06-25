using Practica02.API.Repositories;
using Practica02.API.Models;

namespace Practica02.API.Services
{
    public class MascotaService : IMascotaService
    {
        private readonly IMascotaRepository _mascotaRepository;
        private readonly IClienteRepository _clienteRepository;

        public MascotaService(IMascotaRepository mascotaRepository, IClienteRepository clienteRepository)
        {
            _mascotaRepository = mascotaRepository;
            _clienteRepository = clienteRepository;
        }

        public async Task<IEnumerable<Mascota>> ObtenerTodasAsync()
        {
            return await _mascotaRepository.ObtenerTodas();
        }

        public async Task<IEnumerable<MascotaConsulta>> ConsultarMascotasAsync()
        {
            return await _mascotaRepository.ConsultarMascotas();
        }

        public async Task<Mascota?> ObtenerPorIdAsync(long idMascota)
        {
            return await _mascotaRepository.ObtenerPorId(idMascota);
        }

        public async Task<IEnumerable<Mascota>> ObtenerPorIdClienteAsync(long idCliente)
        {
            return await _mascotaRepository.ObtenerPorIdCliente(idCliente);
        }

        public async Task<long> CrearAsync(Mascota mascota)
        {
            if (mascota == null)
                throw new ArgumentNullException(nameof(mascota));

            // Validaciones de negocio
            if (string.IsNullOrWhiteSpace(mascota.Nombre))
                throw new InvalidOperationException("El nombre de la mascota no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(mascota.Especie))
                throw new InvalidOperationException("La especie no puede estar vacía.");

            if (string.IsNullOrWhiteSpace(mascota.Raza))
                throw new InvalidOperationException("La raza no puede estar vacía.");

            if (mascota.Peso <= 0)
                throw new InvalidOperationException("El peso debe ser mayor a 0.");

            // Verificar que el cliente existe
            var cliente = await _clienteRepository.ObtenerPorId(mascota.IdCliente);
            if (cliente == null)
                throw new InvalidOperationException($"No existe cliente con ID {mascota.IdCliente}.");

            var cantidad = await _mascotaRepository.ContarPorEspecie(mascota.Especie);

            if (cantidad >= 2)
                throw new InvalidOperationException("Solo se permiten registrar dos mascotas de la misma especie.");

            return await _mascotaRepository.Crear(mascota);
        }

        public async Task<bool> ActualizarAsync(Mascota mascota)
        {
            if (mascota == null)
                throw new ArgumentNullException(nameof(mascota));

            return await _mascotaRepository.Actualizar(mascota);
        }

        public async Task<bool> EliminarAsync(long idMascota)
        {
            return await _mascotaRepository.Eliminar(idMascota);
        }
    }
}
