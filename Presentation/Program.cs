using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Presentation.Data;
using Presentation.Services;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddDbContext<Context>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));

var app = builder.Build();
app.MapOpenApi();
app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
