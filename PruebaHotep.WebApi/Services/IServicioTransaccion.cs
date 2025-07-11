using PruebaHotep.WebApi.Data;
using PruebaHotep.WebApi.Models;

namespace PruebaHotep.WebApi.Services
{
    public interface IServicioTransaccion
    {
        Task<Transaccion> RegistrarDepositoAsync(string numeroCuenta, decimal monto);

        Task<Transaccion> RegistrarRetiroAsync(string numeroCuenta, decimal monto);

        Task<List<Transaccion>> ObtenerHistorialAsync(string numeroCuenta);
    }
}
