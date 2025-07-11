using Microsoft.AspNetCore.Mvc;
using PruebaHotep.WebApi.DTOs;
using PruebaHotep.WebApi.Models;
using PruebaHotep.WebApi.Services;

namespace PruebaHotep.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IServicioCliente _servicioCliente;

        //constructor 
        public ClientesController(IServicioCliente servicioCliente)
        {
            _servicioCliente = servicioCliente;
        }

        //endpoints
        [HttpPost]
        public async Task<ActionResult<Cliente>> CrearCliente([FromBody] ClienteCrearDTO dto)
        {
            var cliente = new Cliente
            {
                Nombre = dto.Nombre,
                FechaNacimiento = dto.FechaNacimiento,
                Sexo = dto.Sexo,
                Ingresos = dto.Ingresos
            };

            var ClienteCreado = await _servicioCliente.CrearClienteAsync(cliente);
            return Ok(ClienteCreado);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDTO>> ObtenerClientePorId(int id)
        {
            var cliente = await _servicioCliente.ObtenerClientePorIdAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }


            var clienteDto = new ClienteDTO
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                FechaNacimiento = cliente.FechaNacimiento,
                Sexo = cliente.Sexo,
                Ingresos = cliente.Ingresos,
                Cuentas = cliente.Cuentas?.Select(c => new CuentaResumidaDTO
                {
                    NumeroCuenta = c.NumeroCuenta,
                    SaldoInicial = c.SaldoInicial
                }).ToList()
            };

            return Ok(clienteDto);
        }
    }
}
