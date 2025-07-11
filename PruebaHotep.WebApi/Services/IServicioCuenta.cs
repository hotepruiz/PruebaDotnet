using PruebaHotep.WebApi.Data;
using PruebaHotep.WebApi.Models;

namespace PruebaHotep.WebApi.Services
{
    public interface IServicioCuenta
    {
        Task<Cuenta> CrearCuentaAsync(int clienteId, decimal SaldoInicial);

        Task<Cuenta?> ObtenerCuentaYRegistroPorNumeroAsync(string numeroCuenta);

        Task<decimal?> ObtenerSaldoInicialAsync(string NumeroCuenta);

        Task<decimal?> ObtenerSaldoActualAsync(string NumeroCuenta);
    }
}
