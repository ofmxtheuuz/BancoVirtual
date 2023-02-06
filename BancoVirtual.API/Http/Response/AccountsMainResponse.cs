namespace BancoVirtual.API.Http.Response;

public class AccountsMainResponse
{
    public int Code { get; set; }
    public string Status { get; set; }
    public dynamic? Contas { get; set; }
}