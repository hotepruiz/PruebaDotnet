namespace PruebaHotep.WebApi.Models
{
    public class Transaccion
    {
        public int Id { get; set; }

        public DateTime FechaTransaccion { get; set; } = DateTime.UtcNow;

        public string Tipo { get; set; }  

        public decimal Monto { get; set; }

        public decimal SaldoResultante { get; set; }


        // clave foranea la cuenta
        public int CuentaId { get; set; }
        public Cuenta Cuenta { get; set; }

    }
}
