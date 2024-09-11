using ecommerce.Infrastructure;
using ecommerce.Infrastructure.Options.Crypto;
using ecommerce.Persistence;
using ecommerce.Persistence.Options;

var builder = WebApplication.CreateBuilder(args);

//~ Begin - Configurations
var connectionStringsConfig = builder.Configuration.GetSection(nameof(ConnectionStrings));
var pbkdf2Config = builder.Configuration.GetSection($"{nameof(CryptoOptions)}:{nameof(CryptoOptions.PBKDF2)}");
var aesConfig = builder.Configuration.GetSection($"{nameof(CryptoOptions)}:{nameof(CryptoOptions.AES)}");

builder.Services.Configure<ConnectionStrings>(connectionStringsConfig);
builder.Services.Configure<CryptoOptions.PBKDF2>(pbkdf2Config);
builder.Services.Configure<CryptoOptions.AES>(aesConfig);
//~ End

//~ Begin - Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistenceServices(connectionStringsConfig.Get<ConnectionStrings>());
builder.Services.AddInfrastructureServices(pbkdf2Config.Get<CryptoOptions.PBKDF2>(),
    aesConfig.Get<CryptoOptions.AES>());
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