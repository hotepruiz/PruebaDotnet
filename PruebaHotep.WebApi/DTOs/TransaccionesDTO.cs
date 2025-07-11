namespace PruebaHotep.WebApi.DTOs
{
    public class TransaccionCrearDTO
    {
        public string NumeroCuenta { get; set; }
        public decimal Monto { get; set; }
    }

    public class TransaccionDTO
    {
        public int Id { get; set; }
        public DateTime FechaTransaccion { get; set; }
        public string Tipo { get; set; }
        public decimal Monto { get; set; }
        public decimal SaldoResultante { get; set; }
    }
}