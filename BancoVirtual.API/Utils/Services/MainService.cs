using BancoVirtual.API.Database;
using BancoVirtual.API.Http.Request;
using BancoVirtual.API.Http.Response;
using BancoVirtual.API.Models;
using BancoVirtual.API.Utils.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BancoVirtual.API.Utils.Services;

// Services for Main Controller
public class MainService : IMainService
{
    // constructor
    public MainService(sqlserverdbcontext context)
    {
        this.context = context;
    }

    public sqlserverdbcontext context { get; set; }
    
    //      /v3/contas
    public async Task<List<Conta>> Accounts()
    {
        return await context.Contas.ToListAsync();
    }

    //      /v3/registrar
    public async Task<dynamic> Registrar(RegisterRequest req)
    {
        try
        {
            if (req.Titular != null && req.Email != null)
            {
                var math = new Random();
                var numero = math.Next(100000, 999999);
                var conta = new Conta()
                {
                    Email = req.Email,
                    Titular = req.Titular,
                    Saldo = (float)0.0,
                    Numero = numero.ToString()
                };

                await context.Contas.AddAsync(conta);
                await context.SaveChangesAsync();

                return conta;  
            }
            else
            {
                return false;
            }
        }
        catch (Exception e)
        {
            return false;
        }
        
    }

    //      /v3/transferir
    public async Task<bool> Transferir(TransferRequest req)
    {
        try
        {
            var conta1 = await context.Contas.FirstOrDefaultAsync(x => x.Email.ToLower().Equals(req.Email.ToLower()));
            var conta2 = await context.Contas.FirstOrDefaultAsync(x => x.Numero.Equals(req.Numero));

            if (conta1 != null && conta2 != null)
            {
                var conta1_saldo = conta1.Saldo;
                if (req.Quantidade > 0 && conta1_saldo >= req.Quantidade)
                {
                    conta1.Saldo -= req.Quantidade;
                    conta2.Saldo += req.Quantidade;

                    await context.Extratos.AddAsync(new()
                    {
                        Transferido = int.Parse(conta2.Numero),
                        Transferidor = int.Parse(conta1.Numero),
                        Valor = req.Quantidade,
                        DataDaAcao = DateTime.Now
                    });
                    await context.SaveChangesAsync();
                    return true;

                }
                else
                {
                    return false;
                }
        }
            else
            {
                return false;
            }

            
        }
        catch (Exception e)
        {
            return false;
        }
    }

    //      /v3/conta
    public async Task<Conta> Usuario(UsuarioRequest req)
    {
        return await context
            .Contas
            .FirstOrDefaultAsync(x => x
                .Titular
                .ToLower()
                .Equals(req
                    .Titular
                    .ToLower()));
    }
    
    //      /v3/extrato

    public async Task<dynamic> Extrato(ExtratoRequest req)
    {
        var conta = await context.Contas.FirstOrDefaultAsync(x => x.Email.ToLower().Equals(req.Email.ToLower()));
        if (conta != null)
        {
            var newExtrato = new List<ExtratoResponse>();
            var extratos = await context.Extratos.Where(x => x.Transferidor == int.Parse(conta.Numero)).ToListAsync();
            foreach (var extrato in extratos)
            {
                var transferido = await context.Contas.FirstOrDefaultAsync(x => x.Numero == extrato.Transferido.ToString());
                if (transferido == null) break;
                newExtrato.Add(new()
                {
                    Valor = extrato.Valor,
                    DataDeTransacao = extrato.DataDaAcao.ToString("dddd, dd MMMM yyyy HH:mm:ss"),
                    Tranferido = new() { Numero = transferido.Numero, Titular = transferido.Titular },
                    Tranferidor = new() { Numero = conta.Numero, Titular = conta.Titular }
                });
            }

            return newExtrato;
        }
        else
        {
            return false;
        }
    }
}