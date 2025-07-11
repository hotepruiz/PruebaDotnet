using Microsoft.AspNetCore.Mvc;
using PruebaHotep.WebApi.Models;
using PruebaHotep.WebApi.Services;

namespace PruebaHotep.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransaccionesController : ControllerBase
    {
        private readonly IServicioTransaccion _servicioTransaccion;

        public TransaccionesController(IServicioTransaccion servicioTransaccion)
        {
            _servicioTransaccion = servicioTransaccion;
        }

        [HttpPost("deposito")]
        public async Task<ActionResult<Transaccion>> RealizarDeposito(string numeroCuenta, decimal monto)
        {
            try
            {
                var transaccion = await _servicioTransaccion.RegistrarDepositoAsync(numeroCuenta, monto);
                return Ok(transaccion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("retiro")]
        public async Task<ActionResult<Transaccion>> RealizarRetiro(string numeroCuenta, decimal monto)
        {
            try
            {
                var transaccion = await _servicioTransaccion.RegistrarRetiroAsync(numeroCuenta, monto);
                return Ok(transaccion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{numeroCuenta}")]
        public async Task<ActionResult<List<Transaccion>>> ObtenerHistorial(string numeroCuenta)
        {
            try
            {
                var historial = await _servicioTransaccion.ObtenerHistorialAsync(numeroCuenta);
                return Ok(historial);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
