using ecommerce.Persistence;
using ecommerce.Persistence.Options;

var builder = WebApplication.CreateBuilder(args);

//~ Begin - Configurations
builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection(nameof(ConnectionStrings)));
//~ End

//~ Begin - Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistenceServices(builder.Configuration.GetSection(nameof(ConnectionStrings)).Get<ConnectionStrings>());
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