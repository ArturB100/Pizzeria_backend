
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Pizzeria.Data;
using Pizzeria.Services;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Pizzeria.Dto;
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


            /*IServiceCollection services = builder.Services;
            builder.Services.AddAutoMapper(typeof(Program));

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Foo, FooDto>();
                cfg.CreateMap<Bar, BarDto>();
            });

            var mapper = configuration.CreateMapper();*/



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
            }));



            // container IoC
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<JWTauthService>();

            builder.Services.AddScoped<AddressService>();

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


            app.MapControllers();

            app.Run();
        }
    }
}
