using Contexts;
using Microsoft.EntityFrameworkCore;
using Repositories;

DotNetEnv.Env.Load("database.env");
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductRepo, MySqlRepo>();

try
{
    // get credentials from .env file
    String? server = Environment.GetEnvironmentVariable("MYSQL_SERVER");
    String? database = Environment.GetEnvironmentVariable("MYSQL_DATABASE");
    String? user = Environment.GetEnvironmentVariable("MYSQL_USER");
    String? password = Environment.GetEnvironmentVariable("MYSQL_PASSWORD");

    //check if empty creds
    if (server == null || database == null || user == null || password == null)
    {
        throw new Exception("Connection cretendials to MySQL server were not correct!");
    }

    //build conn string
    String _connectionString = "server=" + server + ";database=" + database + ";user=" + user + ";password=" + password;

    builder.Services.AddDbContext<ProductsContext>(opt => opt.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString)));
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
//app.UseAuthorization();

app.UseStaticFiles();
app.MapControllers();
app.Run();
