using BancoVirtual.API.Database;
using BancoVirtual.API.Http.Request;
using BancoVirtual.API.Models;

namespace BancoVirtual.API.Utils.Services.Interfaces;

public interface IMainService
{

    public Task<List<Conta>> Accounts();
    
    public Task<dynamic> Registrar(RegisterRequest req);
    public Task<bool> Transferir(TransferRequest req);
    public Task<Conta> Usuario(UsuarioRequest req);
    public Task<dynamic> Extrato(ExtratoRequest req);
}