using TableBooking.Data;
using TableBooking.Interfaces;
using TableBooking.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TableBooking
{
    public class Startup
    {
        // Configuration instance to access configuration settings
        private readonly IConfiguration _configuration;

        // Constructor to initialize the configuration
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>
                (options => options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers();
                //.AddNewtonsoftJson(options =>
                //{
                //    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                //    options.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;
                //});

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddHttpClient();
            services.AddSwaggerGen();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IBookingRepository, BookingRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
        {
            // Configure the HTTP request pipeline.
            if (environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
