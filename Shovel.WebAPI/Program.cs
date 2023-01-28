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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ShovelContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("Shovel")));


builder.Services.AddTransient<IPerformanceSystemSynchronizeService, PerformanceSystemSynchronizeService>();
builder.Services.AddTransient<IApplicationSystemSynchronizeService, ApplicationSystemSynchronizeService>();
builder.Services.AddTransient<ISynchronizationService, SynchronizationService>();

builder.Services.AddTransient<IPerformanceSystemDataService, PerformanceSystemDataService>();
builder.Services.AddTransient<IApplicationSystemDataService, ApplicationSystemDataService>();


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
