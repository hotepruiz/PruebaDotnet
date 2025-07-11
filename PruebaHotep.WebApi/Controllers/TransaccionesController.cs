﻿using Microsoft.AspNetCore.Mvc;
using PruebaHotep.WebApi.DTOs;
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

        //asdasdsa
        [HttpPost("deposito")]
        public async Task<ActionResult<TransaccionCrearDTO>> RealizarDeposito([FromBody] TransaccionCrearDTO dto)
        {


            var transaccion = await _servicioTransaccion.RegistrarDepositoAsync(dto.NumeroCuenta, dto.Monto);

            var transaccionDTO = new TransaccionDTO
            {
                Id = transaccion.Id,
                Tipo = transaccion.Tipo,
                FechaTransaccion = transaccion.FechaTransaccion,
                Monto = transaccion.Monto,
                SaldoResultante = transaccion.SaldoResultante
            };

            return Ok(transaccionDTO);
        }

        [HttpPost("retiro")]
        public async Task<ActionResult<TransaccionDTO>> RealizarRetiro([FromBody] TransaccionCrearDTO dto)
        {

            try
            {
                var transaccion = await _servicioTransaccion.RegistrarRetiroAsync(dto.NumeroCuenta, dto.Monto);

                var transaccionDTO = new TransaccionDTO
                {
                    Id = transaccion.Id,
                    Tipo = transaccion.Tipo,
                    FechaTransaccion = transaccion.FechaTransaccion,
                    Monto = transaccion.Monto,
                    SaldoResultante = transaccion.SaldoResultante
                };

                return Ok(transaccionDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/Historial/{numeroCuenta}")]
        public async Task<ActionResult<List<TransaccionDTO>>> ObtenerHistorial(string numeroCuenta)
        {
            try
            {
                var historial = await _servicioTransaccion.ObtenerHistorialAsync(numeroCuenta);

                if (historial == null || historial.Count == 0)
                    return NotFound("No se encontraron transacciones para esta cuenta.");

                var historialDTO = historial
                    .Select(t => new TransaccionDTO{

                        Id = t.Id,
                        Tipo = t.Tipo,
                        FechaTransaccion = t.FechaTransaccion,
                        Monto = t.Monto,
                        SaldoResultante = t.SaldoResultante
                    }).ToList();


                return Ok(historialDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
