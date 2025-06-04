using Microsoft.OpenApi.Models;
using BitTech.Vendas.Api.Application.Setup;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAutoMapperConfig();
builder.Services.AddDependencyConfig();
builder.Services.AddValidators();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "Bit tech Vendas WebApi NET 9.0.",
            Version = "v1",
            Description = "Desafio de código/lógica Contratação de seguro",
            Contact = new OpenApiContact
            {
                Name ="Luiz Eduardo de Lima",
                Email ="luizedu_lima@hotmail.com"
            }
        
        });
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.ConfigureErrorHandling();
app.Run();
