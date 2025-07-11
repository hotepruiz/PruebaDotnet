using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PruebaHotep.WebApi.Models
{
    public class Cuenta
    {
        public int Id { get; set; }

        [Required]
        public string NumeroCuenta { get; set; }
        
        public decimal SaldoInicial { get; set; }

        //uso esto como clave foranea al cliente
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }//no se si esto es necesario

        //1 cuenta a muchas transacciones 
        public ICollection<Transaccion> Transacciones { get; set; }
    }
}
