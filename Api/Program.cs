
using Aplicacion.Data;
using Aplicacion.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Services
builder.Services.AddOptions();
builder.Services.AddDbContext<DemoContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("dbConnection"));
});
builder.Services.AddScoped<IServiceEf, ServiceEf>();
builder.Services.AddScoped<IServiceDpr, ServiceDpr>();
builder.Services.AddScoped<IServiceRpo, ServiceRpo>();
builder.Services.AddScoped<IServiceAdo, ServiceAdo>();



builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
