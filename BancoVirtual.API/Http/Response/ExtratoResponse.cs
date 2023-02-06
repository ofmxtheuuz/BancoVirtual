using BancoVirtual.API.Models;

namespace BancoVirtual.API.Http.Response;

public class ExtratoResponse
{
    public float Valor { get; set; }
    public AccountsResponse Tranferidor { get; set; }
    public AccountsResponse Tranferido { get; set; }
    public string DataDeTransacao { get; set; }
}