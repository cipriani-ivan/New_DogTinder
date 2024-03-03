using DogTinder.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NewDogTinder.EFDataAccessLibrary.DataAccess;
using NewDogTinder.Repository.IRepositories;
using NewDogTinder.Repository.Repositories;
using NewDogTinder.Services.IService;
using NewDogTinder.Services.Service;
using System.Reflection;
using System.Text;

namespace NewDogTinder
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.ReturnHttpNotAcceptable = true;
            }).AddNewtonsoftJson()
            .AddXmlDataContractSerializerFormatters();

            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IDogRepository, DogRepository>();
            services.AddScoped<IPlaceRepository, PlaceRepository>();
            services.AddScoped<IOwnerRepository, OwnerRepository>();

            services.AddTransient<IAppointmentService, AppointmentService>();
            services.AddTransient<IDogService, DogService>();
            services.AddTransient<IOwnerService, OwnerService>();
            services.AddTransient<IPlaceService, PlaceService>();

            services.AddDbContext<NewDogTinderContext>(options =>
            {
                options.UseSqlite(Configuration["ConnectionStrings:NewDogTinder"]);
            });
            services.AddAutoMapper(typeof(Startup));
            services.AddSwaggerGen(setupAction =>
            {
                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

                setupAction.IncludeXmlComments(xmlCommentsFullPath);

                setupAction.AddSecurityDefinition("NewDogTinderInfoApiBearerAuth", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    Description = "Input a valid token to access this API"
                });

                setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "NewDogTinderInfoApiBearerAuth" }
                        }, new List<string>() }
                });
            });

            services.AddAuthentication("Bearer")
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Authentication:Issuer"],
                    ValidAudience = Configuration["Authentication:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(Configuration["Authentication:SecretForKey"]))
                };
            }
            );

            services.AddAuthorization(options =>
            {
                options.AddPolicy("MustBeFromTrondheim", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("city", "Trondheim");
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
