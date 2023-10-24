using Ardalis.Result.AspNetCore;
using Test.Api.Configurations;
using Test.Api.MappingsProfiles;
using Test.Api.Middlewares;
using Test.Core;
using Test.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
builder.Services.AddTestCore(builder.Configuration);
builder.Services.AddTestInfrastructure(builder.Configuration);

builder.Services.AddAuthorization(builder.Configuration);
builder.Services.AddControllers(options => options.AddDefaultResultConvention());
builder.Services.AddValidationConfiguration();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

var app = builder.Build();

app.Services.MigrateSecurityDbContext();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<TenantMiddleware>();

app.MapControllers();

app.Run();
