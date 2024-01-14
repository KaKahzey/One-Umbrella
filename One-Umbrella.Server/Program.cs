
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using OneUmbrella.BLL.Interfaces;
using OneUmbrella.BLL.Services;
using OneUmbrella.DAL.Interfaces;
using OneUmbrella.DAL.Repositories;
using OneUmbrella.Server.Services;
using System.Data;
using System.Globalization;

namespace OneUmbrella.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

                        

            // Add services to the container.

            builder.Services.AddTransient<SqlConnection>(_ => new SqlConnection(builder.Configuration.GetConnectionString("Default")));

            builder.Services.AddTransient<IFavoriteRepository, FavoriteRepository>();
            builder.Services.AddTransient<IGridRepository, GridRepository>();
            builder.Services.AddTransient<IHumanRepository, HumanRepository>();
            builder.Services.AddTransient<IImageRestaurantRepository, ImageRestaurantRepository>();
            builder.Services.AddTransient<IRatingRepository, RatingRepository>();
            builder.Services.AddTransient<IReservationRepository, ReservationRepository>();
            builder.Services.AddTransient<IReservedTableRepository, ReservedTableRepository>();
            builder.Services.AddTransient<IRestaurantRepository, RestaurantRepository>();
            builder.Services.AddTransient<IStructuralElementRepository, StructuralElementRepository>();
            builder.Services.AddTransient<ITableEntityRepository, TableEntityRepository>();

            builder.Services.AddTransient<IFavoriteService, FavoriteService>();
            builder.Services.AddTransient<IGridService, GridService>();
            builder.Services.AddTransient<IImageRestaurantService, ImageRestaurantService>();
            builder.Services.AddTransient<IRatingService, RatingService>();
            builder.Services.AddTransient<IReservationService, ReservationService>();
            builder.Services.AddTransient<IReservedTableService, ReservedTableService>();
            builder.Services.AddTransient<IRestaurantService, RestaurantService>();
            builder.Services.AddTransient<IStructuralElementService, StructuralElementService>();
            builder.Services.AddTransient<ITableEntityService, TableEntityService>();
            builder.Services.AddTransient<IUserService, UserService>();

            builder.Services.AddTransient<TokenService>();

            builder.Services.AddControllers();

            builder.Services.AddCors(opt =>
            {
                opt.AddPolicy("AllowAnyCors", config =>
                {
                    config.AllowAnyOrigin();
                    config.AllowAnyHeader();
                    config.AllowAnyMethod();
                });
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "One Umbrella API",
                    Description = "Transfer all data between back and front",
                    License = new OpenApiLicense
                    {
                        Name = "MIT License",
                    }
                });
                c.AddSecurityDefinition("Auth Token", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Auth Token" }
            },
            new string[] {}
        }
    });
            });

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("AllowAnyCors");

            app.MapControllers();

            app.Run();
        }
    }
}
