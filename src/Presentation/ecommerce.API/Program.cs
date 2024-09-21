using ecommerce.Infrastructure;
using ecommerce.Infrastructure.Options.Crypto;
using ecommerce.Persistence;
using ecommerce.Persistence.Options;

var builder = WebApplication.CreateBuilder(args);

//~ Begin - Configurations
var connectionStringsConfig = builder.Configuration.GetSection(nameof(ConnectionStrings));

builder.Services.Configure<ConnectionStrings>(connectionStringsConfig);
builder.Services.Configure<CryptoOptions.PBKDF2>(builder.Configuration.GetSection($"{nameof(CryptoOptions)}:{nameof(CryptoOptions.PBKDF2)}"));
builder.Services.Configure<CryptoOptions.AES>(builder.Configuration.GetSection($"{nameof(CryptoOptions)}:{nameof(CryptoOptions.AES)}"));
//~ End

//~ Begin - Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistenceServices(connectionStringsConfig.Get<ConnectionStrings>());
builder.Services.AddInfrastructureServices();
//~ End

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();