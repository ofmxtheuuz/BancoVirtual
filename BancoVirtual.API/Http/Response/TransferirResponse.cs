namespace BancoVirtual.API.Http.Response;

public class TransferirResponse
{
    public int Code { get; set; }
    public string Status { get; set; }
    public AccountsResponse? Transferidor { get; set; }
    public AccountsResponse? Transferido { get; set; }
    public float? Valor { get; set; }
}