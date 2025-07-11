using Xunit;
using PruebaHotep.WebApi.Models;
using PruebaHotep.WebApi.Services;
using PruebaHotep.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace PruebaHotep.Tests
{
    public class CuentaTests
    {
        private ContextoBD GetContextoInMemory() //simular la base de datos
        {
            var options = new DbContextOptionsBuilder<ContextoBD>()
                .UseInMemoryDatabase(databaseName: "TestDbCuentas")
                .Options;
            return new ContextoBD(options);
        }

        [Fact]
        public async Task TestCrearCuenta()
        {
            // preludio
            var contextoCrearCuenta = GetContextoInMemory();
            var servicioCliente = new ServicioCliente(contextoCrearCuenta);
            var nuevoCliente = await servicioCliente.CrearClienteAsync(new Cliente
            {
                Nombre = "Test",
                FechaNacimiento = DateTime.Now.AddYears(-30),
                Sexo = "Masculino",
                Ingresos = 1000
            });

            var servicioCuenta = new ServicioCuenta(contextoCrearCuenta, servicioCliente);

            
            var cuenta = await servicioCuenta.CrearCuentaAsync(nuevoCliente.Id, 500);

            // testear
            Assert.NotNull(cuenta);
            Assert.Equal(500, cuenta.SaldoInicial);
        }

        [Fact]
        public async Task TestConsultarSaldo()
        {
            // preludio
            var contextoConsultarSaldo = GetContextoInMemory();
            var servicioCliente = new ServicioCliente(contextoConsultarSaldo);
            var nuevoCliente = await servicioCliente.CrearClienteAsync(new Cliente
            {
                Nombre = "Test",
                FechaNacimiento = DateTime.Now.AddYears(-30),
                Sexo = "Masculino",
                Ingresos = 1000
            });
            
            var servicioCuenta = new ServicioCuenta(contextoConsultarSaldo, servicioCliente);
             
            
            var cuenta = await servicioCuenta.CrearCuentaAsync(nuevoCliente.Id, 500);

            var saldo = await servicioCuenta.ObtenerSaldoActualAsync(cuenta.NumeroCuenta);

            // testear
            Assert.Equal(500, saldo);
        }
    }
}
