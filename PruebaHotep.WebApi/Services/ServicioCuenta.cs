using Microsoft.EntityFrameworkCore;
using PruebaHotep.WebApi.Data;
using PruebaHotep.WebApi.Models;

namespace PruebaHotep.WebApi.Services
{
    public class ServicioCuenta : IServicioCuenta
    {
        private readonly ContextoBD _contexto;
        private readonly IServicioCliente _servicioCliente;

        //constructor
        public ServicioCuenta(ContextoBD contexto, IServicioCliente servicioCliente)
        {
            _contexto = contexto;
            _servicioCliente = servicioCliente;
        }

        //metodos
        public async Task<Cuenta> CrearCuentaAsync(int clienteId, decimal SaldoInicial)
        {
            //verificar que el cliente si existe
            var cliente = await _servicioCliente.ObtenerClientePorIdAsync(clienteId);

            if (cliente == null)
            {
                throw new Exception("El cliente no existe");
            }

            // ESTOY USANDO UN GENERADOR ALEATORIO PARA EL NUMERO DE LA CUENTA
            var numeroCuenta = Guid.NewGuid().ToString().Substring(0, 10);

            var cuenta = new Cuenta
            {
                NumeroCuenta = numeroCuenta,
                ClienteId = clienteId,
                SaldoInicial = SaldoInicial
            };

            _contexto.Cuentas.Add(cuenta);
            await _contexto.SaveChangesAsync();

            return cuenta;
        }

        public async Task<Cuenta?> ObtenerCuentaYRegistroPorNumeroAsync(string numeroCuenta)
        {
            return await _contexto.Cuentas
                .Include(c => c.Transacciones)
                .FirstOrDefaultAsync(c => c.NumeroCuenta == numeroCuenta);
        }


        public async Task<decimal?> ObtenerSaldoInicialAsync(string numeroCuenta)
        {
            var cuenta = await _contexto.Cuentas
                .FirstOrDefaultAsync(c => c.NumeroCuenta == numeroCuenta);

            return cuenta?.SaldoInicial;
        }


        public async Task<decimal?> ObtenerSaldoActualAsync(string numeroCuenta)
        {
            var cuenta = await _contexto.Cuentas
                .Include(c => c.Transacciones)
                .FirstOrDefaultAsync(c => c.NumeroCuenta == numeroCuenta);

            if (cuenta == null)
            {
                return null;
            }

            if (cuenta.Transacciones.Count == 0)
            {
                return cuenta.SaldoInicial;
            }

            decimal saldoActual = cuenta.SaldoInicial;

            foreach (var t in cuenta.Transacciones)
            {
                if (t.Tipo == "Deposito")
                    saldoActual += t.Monto;
                else if (t.Tipo == "Retiro")
                    saldoActual -= t.Monto;
            }

            return saldoActual;
        }
    }
}
