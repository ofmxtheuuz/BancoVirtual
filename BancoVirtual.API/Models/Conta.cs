namespace BancoVirtual.API.Models;

public class Conta
{
    public int ContaId { get; set; }
    public string Titular { get; set; }
    public string Email { get; set; }
    public string Numero { get; set; }
    public float Saldo { get; set; }
}