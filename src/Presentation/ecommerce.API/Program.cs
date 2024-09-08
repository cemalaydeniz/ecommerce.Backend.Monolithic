using ecommerce.Persistence;
using ecommerce.Persistence.Options;

var builder = WebApplication.CreateBuilder(args);

//~ Begin - Configurations
var connectionStringConfig = builder.Configuration.GetSection(nameof(ConnectionStrings));

builder.Services.Configure<ConnectionStrings>(connectionStringConfig);
//~ End

//~ Begin - Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistenceServices(connectionStringConfig.Get<ConnectionStrings>());
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