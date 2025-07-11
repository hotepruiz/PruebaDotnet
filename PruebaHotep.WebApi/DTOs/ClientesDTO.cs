namespace PruebaHotep.WebApi.DTOs
{
    public class ClienteCrearDTO
    {
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        public decimal Ingresos { get; set; }
    }

    public class ClienteDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        public decimal Ingresos { get; set; }

        // Si querés incluir cuentas (resumidamente), podés agregar:
        public List<CuentaResumidaDTO>? Cuentas { get; set; }
    }

    public class CuentaResumidaDTO
    {
        public string NumeroCuenta { get; set; }
        public decimal SaldoInicial { get; set; }
    }

}
