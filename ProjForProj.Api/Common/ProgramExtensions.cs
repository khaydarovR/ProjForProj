using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProjForProj.Api.DAL;
using ProjForProj.Api.Domain.Entity;
using ProjForProj.Api.Services;
using System.Reflection;

namespace ProjForProj.Api.Common;

public static class ProgramExtensions
{
    public static void AddMyServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services
            .AddIdentity<AppUser, IdentityRole<Guid>>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireUppercase = false;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<AppDbContext>();

        builder.Services.AddTransient<IProjectService, ProjectService>();
        builder.Services.AddTransient<IDesigneObjService, DesigneObjService>();
    }


    public static void AddSwaggerGenComment(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API - V1", Version = "v1" });
            var projName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;

            var filePath = Path.Combine(System.AppContext.BaseDirectory, projName+".xml");
            c.IncludeXmlComments(filePath);
        });
    }

}
