namespace BancoVirtual.API.Http.Response;

public class RegisterResponse
{
    public int Code { get; set; }
    public string Status { get; set; }
    public RegisterAccountResponse? Conta { get; set; }
}