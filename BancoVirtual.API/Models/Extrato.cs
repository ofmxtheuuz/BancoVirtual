namespace BancoVirtual.API.Models;

public class Extrato
{
    public int ExtratoId { get; set; }
    public int Transferidor { get; set; }
    
    public int Transferido { get; set; }
    
    public float Valor { get; set; }
    public DateTime DataDaAcao {
        get;
        set;
    }
}