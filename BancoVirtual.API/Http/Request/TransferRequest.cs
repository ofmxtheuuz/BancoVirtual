namespace BancoVirtual.API.Http.Request;

public class TransferRequest
{
    public string Email { get; set; }
    public string Numero { get; set; }
    public float Quantidade { get; set; }
}