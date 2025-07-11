using PruebaHotep.WebApi.Models;

namespace PruebaHotep.WebApi.Services
{
    public interface IServicioCliente
    {
        Task<Cliente> CrearClienteAsync(Cliente cliente);
        Task<Cliente?> ObtenerClientePorIdAsync(int id);
    }
}

