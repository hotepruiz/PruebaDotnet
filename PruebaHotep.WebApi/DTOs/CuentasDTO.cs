namespace PruebaHotep.WebApi.DTOs
{
    public class CuentaCrearDTO
    {
        //aqui se esperaria el numero de cuenta, pero lo decidi crear con un generador
        public int ClienteId { get; set; }
        public decimal SaldoInicial { get; set; }
    }

    public class CuentaDTO
    {
        public int Id { get; set; }
        public string NumeroCuenta { get; set; }
        public decimal SaldoInicial { get; set; }

        public List<TransaccionResumida>? Transacciones { get; set; }
    }

    public class CuentaSaldoDTO
    {
        public decimal? Saldo { get; set; }
    }

    public class TransaccionResumida
    {
        public string Tipo { get; set; }
        public decimal Monto { get; set; }
    }
}