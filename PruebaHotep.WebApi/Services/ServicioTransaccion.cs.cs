using Microsoft.EntityFrameworkCore;
using PruebaHotep.WebApi.Data;
using PruebaHotep.WebApi.Models;

namespace PruebaHotep.WebApi.Services
{
    public class ServicioTransaccion : IServicioTransaccion
    {
        private readonly ContextoBD _contexto;
        private readonly IServicioCuenta _servicioCuenta;

        //constructor
        public ServicioTransaccion (ContextoBD contexto, IServicioCuenta servicioCliente)
        {
            _contexto = contexto;
            _servicioCuenta = servicioCliente;
        }

        //metodos
        public async Task<Transaccion> RegistrarDepositoAsync(string numeroCuenta, decimal monto) {
            
            var cuenta = await _servicioCuenta.ObtenerCuentaYRegistroPorNumeroAsync(numeroCuenta);

            if (cuenta == null) {
                throw new Exception("Cuenta no encontrada");
            }

            var saldoActual = await _servicioCuenta.ObtenerSaldoActualAsync(numeroCuenta);


            var transaccion = new Transaccion
            {
                CuentaId = cuenta.Id,
                Tipo = "Deposito",
                Monto = monto,
                SaldoResultante = saldoActual.GetValueOrDefault() + monto
            };

            _contexto.Transacciones.Add(transaccion);
            await _contexto.SaveChangesAsync();

            return transaccion;
        }

        public async Task<Transaccion> RegistrarRetiroAsync(string numeroCuenta, decimal monto)
        {

            var cuenta = await _servicioCuenta.ObtenerCuentaYRegistroPorNumeroAsync(numeroCuenta);

            if (cuenta == null)
            {
                throw new Exception("Cuenta no encontrada");
            }

            var saldoActual = await _servicioCuenta.ObtenerSaldoActualAsync(numeroCuenta);

            if (saldoActual == null || saldoActual < monto)
                throw new Exception("Saldo insuficiente para el retiro");

            var transaccion = new Transaccion
            {
                CuentaId = cuenta.Id,
                Tipo = "Retiro",
                Monto = monto,
                SaldoResultante = saldoActual.GetValueOrDefault() - monto
            };

            _contexto.Transacciones.Add(transaccion);
            await _contexto.SaveChangesAsync();

            return transaccion;
        }

        public async Task<List<Transaccion>> ObtenerHistorialAsync(string numeroCuenta)
        {
            var cuenta = await _servicioCuenta.ObtenerCuentaYRegistroPorNumeroAsync(numeroCuenta);
            if (cuenta == null)
                throw new Exception("Cuenta no encontrada");

            return cuenta.Transacciones.OrderByDescending(t => t.Id).ToList();
        }

    }
}
