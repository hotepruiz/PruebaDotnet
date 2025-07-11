using Microsoft.AspNetCore.Mvc;
using PruebaHotep.WebApi.DTOs;
using PruebaHotep.WebApi.Models;
using PruebaHotep.WebApi.Services;

namespace PruebaHotep.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CuentasController : ControllerBase
    {
        private readonly IServicioCuenta _servicioCuenta;

        public CuentasController(IServicioCuenta servicioCuenta)
        {
            _servicioCuenta = servicioCuenta;
        }

        //hola
        [HttpPost]
        public async Task<ActionResult<CuentaDTO>> CrearCuenta([FromBody] CuentaCrearDTO cuentadto)
        {
            var cuenta = await _servicioCuenta.CrearCuentaAsync(cuentadto.ClienteId, cuentadto.SaldoInicial);

            var cuentaDTO = new CuentaDTO
            {
                Id = cuenta.Id,
                NumeroCuenta = cuenta.NumeroCuenta,
                SaldoInicial = cuenta.SaldoInicial,
                Transacciones = new List<TransaccionResumida>()  // si está vacía al principio
            };

            return Ok(cuentaDTO);
        }


        [HttpGet("{numeroCuenta}")]
        public async Task<ActionResult<CuentaDTO>> ObtenerCuenta(string numeroCuenta)
        {
            var cuenta = await _servicioCuenta.ObtenerCuentaYRegistroPorNumeroAsync(numeroCuenta);
            if (cuenta == null)
                return NotFound();

            var cuentaDTO = new CuentaDTO
            {
                Id = cuenta.Id,
                NumeroCuenta = cuenta.NumeroCuenta,
                SaldoInicial = cuenta.SaldoInicial,
                Transacciones = cuenta.Transacciones.Select(c => new TransaccionResumida
                {
                    Tipo = c.Tipo,
                    Monto = c.Monto
                }).ToList()

            };

            return Ok(cuentaDTO);
        }

        [HttpGet("saldo/{numeroCuenta}")]
        public async Task<ActionResult<CuentaSaldoDTO?>> ObtenerSaldoActual(string numeroCuenta)
        {
            var saldo = await _servicioCuenta.ObtenerSaldoActualAsync(numeroCuenta);

            if (saldo == null)
                return NotFound();

            var saldoDTO = new CuentaSaldoDTO
            {
                Saldo = saldo
            };

            return saldoDTO;
        }
    }
}
