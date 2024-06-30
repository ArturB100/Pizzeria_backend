
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Pizzeria.Data;
using Pizzeria.Services;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Pizzeria.Dto;
using Pizzeria.Dto.Request;
using Pizzeria.Model;

namespace Pizzeria
{
    public class Program
    {

       
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddDbContext<PizzeriaContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("PizzeriaContext")));   

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("qD3zq5CnH9vZfL0ZRQkZkDyxFbLz9WyKFesT9yXpbNs=\r\n"))
                };
            });



            // Add AutoMapper as a singleton
            builder.Services.AddSingleton<IMapper>(sp => new Mapper(sp.GetRequiredService<MapperConfiguration>(), sp.GetService));

            // Configure AutoMapper
            builder.Services.AddSingleton(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<NewUserDtoReq, User>();
                cfg.CreateMap<NewAddressDto, Address>();
                cfg.CreateMap<User, UserShowDataDto>()
                    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));
                cfg.CreateMap<Address, Address>();
                cfg.CreateMap<Address, Address>();
            }));


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("SpecificOriginPolicy",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200") // Specify the allowed host
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });


            // container IoC
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<JWTauthService>();

            builder.Services.AddScoped<AddressService>();
            builder.Services.AddScoped<PizzaService>();
            builder.Services.AddScoped<IngredientService>();
            builder.Services.AddScoped<OrderService>();
            
            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                SeedData.Initialize(scope.ServiceProvider);
            }



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("SpecificOriginPolicy");

            app.MapControllers();

            app.Run();
        }
    }
}
