using BancoVirtual.API.Database;
using BancoVirtual.API.Utils.Services;
using BancoVirtual.API.Utils.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddTransient<IMainService, MainService>();
builder.Services.AddDbContext<sqlserverdbcontext>(options => options.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=BancoVirtual"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();