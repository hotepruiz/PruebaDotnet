using System;
using System.Collections.Generic;

namespace PruebaHotep.WebApi.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string Sexo {  get; set; }

        public decimal Ingresos {  get; set; }

        //1 a muhcos
        public ICollection<Cuenta> Cuentas { get; set; } 
    }
}
