using GameOfLife.Core;
using GameOfLife.Core.IServices;
using GameOfLife.Data.Access.Services;
using GameOfLife.Data.SQL;
using GameOfLife.Web.API.BackgroundServices;
using GameOfLife.Web.API.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, lc) => lc
.ReadFrom.Configuration(context.Configuration)
);


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<ApiContext>(options => options.UseInMemoryDatabase("GameOfLife"));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient<IUnitOfWork,UnitOfWork>();
builder.Services.AddTransient<IGameService,GameService>();
builder.Services.AddHostedService<GamesMemoryHandler>();
builder.Services.AddSignalR();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseCors();
app.UseHttpsRedirection();
app.UseSerilogRequestLogging();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
app.MapHub<GameHub>("/GameHub");
app.Run();
