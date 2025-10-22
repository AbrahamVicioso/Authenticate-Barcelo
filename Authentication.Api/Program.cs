using Authentication.Domain.Entities;
using Authentication.Persistence;
using Authentication.Persistence.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddPersistenceLayer();

builder.Services
    .AddAuthentication()
    .AddBearerToken(IdentityConstants.BearerScheme);

builder.Services
    .AddIdentityCore<User>(options => options.SignIn.RequireConfirmedEmail = false)
    .AddEntityFrameworkStores<AuthenticationDbContext>()
    .AddApiEndpoints();

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapIdentityApi<User>();

app.MapControllers();

app.Run();