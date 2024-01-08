using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PassengerTimeTracker.API.AutoMapper;
using PassengerTimeTracker.Business.Drivers;
using PassengerTimeTracker.Business.TripFacade;
using PassengerTimeTracker.Data;
using PassengerTimeTracker.Data.EF.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMemoryCache();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new DtoToEntityProfile());
});

var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddDbContext<PassengerTimeTrackerContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("PassangerTimeTrackerDB"))
);

builder.Services.AddScoped<ITripRepository, TripRepository>();
builder.Services.AddScoped<IDriverRepository, DriverRepository>();

builder.Services.AddScoped<ITripFacade, TripFacade>();
builder.Services.AddScoped<IDriverFacade, DriverFacade>();


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:4200")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors();

app.Run();