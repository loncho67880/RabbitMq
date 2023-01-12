using Common.EmailHelper;
using Common.ReadTemplateHelper;
using Notificaciones.Consumer;
using Servicios.AnulacionServicio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IEmailHelper, EmailHelper>();
builder.Services.AddSingleton<IReadTemplateHelper, ReadTemplateHelper>();
builder.Services.AddSingleton<IAnulacionServicio, AnulacionServicio>();

//Add consumers
builder.Services.AddHostedService<IncapacidadConsumer>();

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
