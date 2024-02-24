using DogTinder.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using NewDogTinder.EFDataAccessLibrary.DataAccess;
using NewDogTinder.Repository.IRepositories;
using NewDogTinder.Repository.Repositories;
using NewDogTinder.Services.IService;
using NewDogTinder.Services.Service;

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
            services.AddControllersWithViews();

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
            services.AddSwaggerGen();
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
