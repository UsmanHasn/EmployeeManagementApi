using BusinessLogicLayer.Helper;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer;
using DataAccessLayer.DbContexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using DataAccessLayer.Interfaces;
using DataAccessLayer;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<Dbcontext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("defaultString")));

builder.Services.AddScoped<IDAL_Auth, DAL_Auth>();
builder.Services.AddScoped<IBLL_Auth, BLL_Auth>();
builder.Services.AddScoped<IGeneralFunctions, GeneralFunctions>();
builder.Services.AddScoped<IBLL_LoggedInUser, BLL_LoggedInUser>();
builder.Services.AddScoped<IEmailHelper, EmailHelper>();
builder.Services.AddScoped<IDAL_Attendence, DAL_Attendence>();
builder.Services.AddScoped<IBLL_Attendence, BLL_Attendence>();
builder.Services.AddScoped<IDAL_Leave, DAL_Leave>();
builder.Services.AddScoped<IBLL_Leave, BLL_Leave>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddCors(p => p.AddPolicy("CorsPolicy", build =>
{
    build.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
}));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = builder.Configuration["Jwt:Issuer"],
                   ValidAudience = builder.Configuration["Jwt:Issuer"],
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
               };
           });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}
app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
            Path.Combine(builder.Environment.ContentRootPath,"Uploads")),
    RequestPath = "/Uploads" // This is optional; it sets the URL path for accessing the files
}); 

app.UseSwagger();
//Add  swagger code usman
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SchoolManagementApi");
    c.RoutePrefix = "";
});

app.UseAuthorization();


app.MapControllers();

app.Run();
