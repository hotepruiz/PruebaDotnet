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
            if (SaldoInicial < 0)
            {
                throw new Exception("El monto inicial de una cuenta debe ser mayor a 0");
            }

            //crear el id nuevo de la venta
            var ultimaCuenta = await _contexto.Cuentas.OrderByDescending(c => c.Id).FirstOrDefaultAsync();

            int siguienteNumero;
            int ultimoNumero;
            if (ultimaCuenta == null)
            {   
                siguienteNumero = 1;
            }
            else 
            {
                siguienteNumero = int.Parse(ultimaCuenta.NumeroCuenta) + 1;

            }


            // Convertir a string con ceros a la izquierda, por ejemplo: 0000000001
            string numeroCuenta = siguienteNumero.ToString("D10");

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
