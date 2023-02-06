using BancoVirtual.API.Database;
using BancoVirtual.API.Http.Request;
using BancoVirtual.API.Http.Response;
using BancoVirtual.API.Models;
using BancoVirtual.API.Utils.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;

namespace BancoVirtual.API.Controllers;

[Route("v3")]
public class MainController : Controller
{
    [HttpGet("contas")]
    public async Task<AccountsMainResponse> Accounts([FromServices] IMainService service)
    {
        try
        {
            var newAccounts = new List<AccountsResponse>();
            var accounts = await service.Accounts();
            foreach (var account in accounts)
            {
                newAccounts.Add(new()
                {
                    Numero = account.Numero,
                    Titular = account.Titular
                });
            }

            return new AccountsMainResponse()
            {
                Code = 200,
                Status = "Requisição aprovada",
                Contas = newAccounts
            };
        }
        catch (Exception e)
        {
            return new AccountsMainResponse()
            {
                Code = 500,
                Status = "Um erro ocorreu"
            };
        }
    }
    
    [HttpGet("extrato")]
    public async Task<ExtratoMainResponse> Extrato([FromServices] IMainService service, [FromBody] ExtratoRequest req)
    {
        try
        {
            var result = await service.Extrato(req);
            if (result is List<ExtratoResponse>)
            {
                return new()
                {
                    Code = 200,
                    Extratos = result,
                    Status = "Requisição aprovada"
                };
            }
            else
            {
                return new()
                {
                    Code = 500,
                    Status = "Um erro ocorreu"
                };
            }
        }
        catch (Exception e)
        {
            return new()
            {
                Code = 500,
                Status = "Um erro ocorreu"
            };
        }
    }
    
    [HttpGet("conta")]
    public async Task<AccountsMainResponse> Usuario([FromServices] IMainService service, [FromBody] UsuarioRequest req)
    {
        try
        {
            var result = await service.Usuario(req);
            if (result != null)
            {
                return new()
                {
                    Contas = result,
                    Code = 200,
                    Status = "Requisição aprovada"
                };
            }
            else
            {
                return new()
                {
                    Contas = null,
                    Code = 404,
                    Status = "Usuário não encontrado" 
                }; 
            }
        }
        catch (Exception e)
        {
            return new()
            {
                Contas = null,
                Code = 500,
                Status = "Um erro ocorreu" 
            };
        }
    }

    [HttpPost("registrar")]
    public async Task<RegisterResponse> Registrar([FromBody] RegisterRequest req, [FromServices] IMainService service)
    {
        try
        {
            var result = await service.Registrar(req);
            if (result is Conta)
            {
                Conta conta = result;
                return new()
                {
                    Code = 201,
                    Status = "Usuário criado com sucesso",
                    Conta = new()
                    {
                        Email = conta.Email,
                        Numero = conta.Numero,
                        Titular = conta.Titular
                    }
                };
            }
            else
            {
                return new()
                {
                    Code = 500,
                    Status = "Um erro ocorreu"
                };
            }
        }
        catch (Exception e)
        {
            return new()
            {
                Code = 500,
                Status = "Um erro ocorreu"
            };
        }
    }

    [HttpPut("transferir")]
    public async Task<TransferirResponse> Transferir([FromServices] IMainService service,
        [FromServices] sqlserverdbcontext context, [FromBody] TransferRequest request)
    {
        try
        {
            var result = await service.Transferir(request);
            if (result)
            {
                var conta1 =
                    await context.Contas.FirstOrDefaultAsync(x => x.Email.ToLower().Equals(request.Email.ToLower()));
                var conta2 = await context.Contas.FirstOrDefaultAsync(x => x.Numero.Equals(request.Numero));

                return new()
                {
                    Code = 200,
                    Status = "Requisição aprovada",
                    Transferidor = new AccountsResponse()
                        { Numero = conta1.Numero, Titular = conta1.Titular },
                    Transferido = new AccountsResponse()
                        { Numero = conta2.Numero, Titular = conta2.Titular },
                    Valor = request.Quantidade
                };
            }
            else
            {
                return new()
                {
                    Code = 500,
                    Status = "Um erro ocorreu",
                    Transferidor = null,
                    Transferido = null,
                    Valor = null
                };
            }
        }
        catch (Exception e)
        {
            return new()
            {
                Code = 500,
                Status = "Um erro ocorreu",
                Transferidor = null,
                Transferido = null,
                Valor = null
            };
        }
    }
}