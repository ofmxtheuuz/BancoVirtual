using BancoVirtual.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BancoVirtual.API.Database;

public class sqlserverdbcontext : DbContext
{
    public sqlserverdbcontext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Conta> Contas { get; set; }
    public DbSet<Extrato> Extratos { get; set; }
}