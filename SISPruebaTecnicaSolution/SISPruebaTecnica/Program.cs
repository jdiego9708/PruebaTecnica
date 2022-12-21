using SISPruebaTecnica.AccesoDatos.Dacs;
using SISPruebaTecnica.AccesoDatos.Interfaces;
using System.IO;
using System.Net.NetworkInformation;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//Inyectar servicios 
builder.Services.AddTransient<IConexionDac, ConexionDac>()
    .AddTransient<IUsuariosDac, UsuariosDac>();

//Leer archivo de configuración appsettings.json y cargarlo en un IConfiguration para inyectarlo
Assembly GetAssemblyByName(string name)
{
    return AppDomain.CurrentDomain.GetAssemblies().
           SingleOrDefault(assembly => assembly.GetName().Name == name);
}

var a = GetAssemblyByName("SISPruebaTecnica");

var stream = a.GetManifestResourceStream("SISPruebaTecnica.appsettings.json");

var config = new ConfigurationBuilder()
            .AddJsonStream(stream)
            .Build();

builder.Configuration.AddConfiguration(config);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
