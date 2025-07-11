using Microsoft.EntityFrameworkCore;
using PruebaHotep.WebApi.Data;
using PruebaHotep.WebApi.Models;

namespace PruebaHotep.WebApi.Services
{
    public class ServicioCliente: IServicioCliente
    {
        private readonly ContextoBD _contexto;

        //cosntructor
        public ServicioCliente(ContextoBD contexto)
        {
            _contexto = contexto;
        }

        //metodos
        public async Task<Cliente> CrearClienteAsync(Cliente cliente)
        {
            _contexto.Clientes.Add(cliente);
            await _contexto.SaveChangesAsync();
            return cliente;
        }

        public async Task<Cliente> ObtenerClientePorIdAsync(int id)
        {
            return await _contexto.Clientes.Include(c => c.Cuentas).FirstOrDefaultAsync(c => c.Id == id);
        }

    }   
}
