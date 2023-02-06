namespace BancoVirtual.API.Http.Response;

public class ExtratoMainResponse
{
    public int Code { get; set; }
    public string Status { get; set; }
    public List<ExtratoResponse>? Extratos { get; set; }
}