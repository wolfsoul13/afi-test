using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using AFI.BusinessLogic.Validators;
using AFI.DataAccess;
using AFI.DataAccess.Repositories;
using AFI.Handlers.Services;
using AFI.Models.Adapters;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Ensures PascalCase

        options.JsonSerializerOptions.TypeInfoResolver = new DefaultJsonTypeInfoResolver
        {
            Modifiers =
            {
                static typeInfo =>
                {
                    if (typeInfo.Kind != JsonTypeInfoKind.Object)
                        return;

                    foreach (JsonPropertyInfo propertyInfo in typeInfo.Properties)
                    {
                        propertyInfo.IsRequired = false;
                    }
                }
            }
        };

        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;

    });
    ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connStr = builder.Configuration.GetConnectionString("AfiDb");

builder.Services.AddDbContextPool<PolicyHolderContext>(options => options.UseSqlServer(connStr));


builder.Services.AddScoped<DbContext, PolicyHolderContext>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPolicyHolderHandler, PolicyHolderHandler>();
builder.Services.AddScoped<IPolicyHolderAdapter, PolicyHolderAdapter>();

builder.Services.AddValidatorsFromAssemblyContaining<PolicyHolderValidator>();

var app = builder.Build();

using (var scope = app.Services.CreateAsyncScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<PolicyHolderContext>();
    dbContext.Database.EnsureCreated();
}

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
