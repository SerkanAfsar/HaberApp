using HaberApp.Core.Models;
using HaberApp.Repository;
using HaberApp.Repository.Configuration;
using HaberApp.ServiceLayer.Configuration;
using HaberApp.WebService.CustomFilters;
using HaberApp.WebService.IdentityValidators;
using HaberApp.WebService.Middlewares;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(opt =>
{
    opt.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
}).ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddMemoryCache();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.Password.RequiredLength = 6;
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = true;
    //options.Password.RequiredLength = 8;
    //options.Password.RequiredUniqueChars = 1;
    //options.Password.RequireLowercase = true;
    //options.Password.RequireUppercase = true;
    //options.Password.RequireNonAlphanumeric = true;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    options.Lockout.MaxFailedAccessAttempts = 5;


}).AddEntityFrameworkStores<AppDbContext>()
.AddErrorDescriber<CustomIdentityErrorDescriber>()
.AddUserValidator<CustomAppUserValidator>()
.AddDefaultTokenProviders();

builder.Services.RegisterIdentityAuthentication(builder.Configuration);

builder.Services.RegisterRepositories();
builder.Services.RegisterServices();
builder.Services.RegisterMapper();
builder.Services.RegisterFluentValidations();
builder.Services.AddScoped(typeof(CustomFilterAttribute<,,>));
builder.Services.AddCors(options => options.AddPolicy("customPolicy", builder =>
{
    builder.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
}));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors("customPolicy");

app.MapControllers();
app.UseMiddleware<CustomExceptionMiddleware>();


app.Run();
