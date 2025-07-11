using Xunit;
using PruebaHotep.WebApi.Models;
using PruebaHotep.WebApi.Services;
using PruebaHotep.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace PruebaHotep.Tests
{
    public class TransaccionesTests
    {
        private ContextoBD GetContextoInMemory() //simular la base de datos
        {
            var options = new DbContextOptionsBuilder<ContextoBD>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            return new ContextoBD(options);
        }

        [Fact]
        public async Task TestearDeposito()
        {
            //preludio
            var contextoDeposito = GetContextoInMemory();
            var servicioCliente = new ServicioCliente(contextoDeposito);
            var servicioCuenta = new ServicioCuenta(contextoDeposito, servicioCliente);
            var servicioTransaccion = new ServicioTransaccion(contextoDeposito, servicioCuenta);

            var cliente = await servicioCliente.CrearClienteAsync(new Cliente
            {
                Nombre = "Hotep Ruiz",
                FechaNacimiento = DateTime.Now,
                Sexo = "Masculino",
                Ingresos = 2000
            });

            var cuenta = await servicioCuenta.CrearCuentaAsync(cliente.Id, 2000);

            var tranasccion = await servicioTransaccion.RegistrarDepositoAsync(cuenta.NumeroCuenta, 500);

            var saldo = await servicioCuenta.ObtenerSaldoActualAsync(cuenta.NumeroCuenta);

            Assert.NotNull(saldo);
            Assert.Equal("Deposito", tranasccion.Tipo);//que sea deposito
            Assert.Equal(2500, saldo);//que el saldo este bien

        }

        [Fact]
        public async Task TestearRetiro()
        {
            //preludio
            var contextoRetiro = GetContextoInMemory();
            var servicioCliente = new ServicioCliente(contextoRetiro);
            var servicioCuenta = new ServicioCuenta(contextoRetiro, servicioCliente);
            var servicioTransaccion = new ServicioTransaccion(contextoRetiro, servicioCuenta);

            var cliente = await servicioCliente.CrearClienteAsync(new Cliente
            {
                Nombre = "Hotep Ruiz",
                FechaNacimiento = DateTime.Now,
                Sexo = "Masculino",
                Ingresos = 2000
            });

            var cuenta = await servicioCuenta.CrearCuentaAsync(cliente.Id, 2000);

            var tranasccion = await servicioTransaccion.RegistrarRetiroAsync(cuenta.NumeroCuenta, 500);

            var saldo = await servicioCuenta.ObtenerSaldoActualAsync(cuenta.NumeroCuenta);

            Assert.NotNull(saldo);
            Assert.Equal("Retiro", tranasccion.Tipo);//que sea retiro
            Assert.Equal(1500, saldo);//que el saldo este bien

        }

        [Fact]
        public async Task TestearHistorial()
        {
            //preludio
            var contextoHistorial = GetContextoInMemory();
            var servicioCliente = new ServicioCliente(contextoHistorial);
            var servicioCuenta = new ServicioCuenta(contextoHistorial, servicioCliente);
            var servicioTransaccion = new ServicioTransaccion(contextoHistorial, servicioCuenta);

            var cliente = await servicioCliente.CrearClienteAsync(new Cliente
            {
                Nombre = "Hotep Ruiz",
                FechaNacimiento = DateTime.Now,
                Sexo = "Masculino",
                Ingresos = 2000
            });

            var cuenta = await servicioCuenta.CrearCuentaAsync(cliente.Id, 2000);



            await servicioTransaccion.RegistrarDepositoAsync(cuenta.NumeroCuenta, 100);
            await servicioTransaccion.RegistrarRetiroAsync(cuenta.NumeroCuenta, 100);

            var historial = await servicioTransaccion.ObtenerHistorialAsync(cuenta.NumeroCuenta);

            Assert.NotNull(historial);
            Assert.Equal(2, historial.Count); // Deben haber dos transacciones
            Assert.Contains(historial, t => t.Tipo == "Deposito");
            Assert.Contains(historial, t => t.Tipo == "Retiro");//qei esten los 2 tipos de transaccion

        }
    }
}
