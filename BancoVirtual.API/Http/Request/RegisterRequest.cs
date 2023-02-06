using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BancoVirtual.API.Http.Request;

public class RegisterRequest
{
    public string Titular { get; set; }
    public string Email { get; set; }
}