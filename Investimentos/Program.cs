using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Adicionar servi�os � cole��o.
builder.Services.AddControllers();

// Configurar o Swagger/OpenAPI
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations(); // Habilita as anota��es, como SwaggerSchema
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "API INVESTIMENTOS", Version = "v1" });
});

var app = builder.Build();

// Configurar o pipeline de requisi��es HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API v1");
        c.RoutePrefix = string.Empty; // Swagger UI estar� na raiz da aplica��o
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
