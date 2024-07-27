using DL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
    //.AddJsonOptions(options =>
    //{
    //    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    //    options.JsonSerializerOptions.MaxDepth = 64; // Puedes ajustar la profundidad según sea necesario
    //});

// Configura la cadena de conexión para el contexto de la base de datos
builder.Services.AddDbContext<InnotecContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "WS-INNOTEC API",
        Version = "v1",
        Description = "API for WS-INNOTEC",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Support",
            Email = "support@wsinnotec.com",
            Url = new Uri("https://wsinnotec.com/support")
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WS-INNOTEC API V1"));
}

// Redirige la raíz a Swagger UI
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", context =>
    {
        context.Response.Redirect("/swagger");
        return Task.CompletedTask;
    });
    endpoints.MapControllers();
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.Run();
