using Microsoft.EntityFrameworkCore;
using Quartz;
using Shovel.WebAPI.Abstractions.Extensions;
using Shovel.WebAPI.Models;
using Shovel.WebAPI.QuartzJobsAndTriggers.Jobs;
using Shovel.WebAPI.Services.Data;
using Shovel.WebAPI.Services.Data.Interfaces;
using Shovel.WebAPI.Services.Synchronize;
using Shovel.WebAPI.Services.Synchronize.Interfaces;
using Newtonsoft.Json;
using Shovel.WebAPI.Services.Configuration.Interfaces;
using Shovel.WebAPI.Services.Configuration;
using Shovel.WebAPI.Services.Report.Interfaces;
using Shovel.WebAPI.Services.Report;
using Shovel.WebAPI.Services.Auth;
using Shovel.WebAPI.Services.Auth.Interfaces;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ShovelContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("Shovel")));


builder.Services.AddTransient<IPerformanceSystemSynchronizeService, PerformanceSystemSynchronizeService>();
builder.Services.AddTransient<IApplicationSystemSynchronizeService, ApplicationSystemSynchronizeService>();
builder.Services.AddTransient<ISynchronizationService, SynchronizationService>();

builder.Services.AddTransient<IPerformanceSystemDataService, PerformanceSystemDataService>();
builder.Services.AddTransient<IApplicationSystemDataService, ApplicationSystemDataService>();

builder.Services.AddTransient<IConfigurationService, ConfigurationService>();

builder.Services.AddTransient<IAuthService, AuthService>();


builder.Services.AddHttpClient();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();

    q.AddJobAndTrigger<SynchronizationJob>(builder.Configuration);
});

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});

// If using IIS:
builder.Services.Configure<IISServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod().WithExposedHeaders("content-disposition"));
app.UseRouting();
app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
