using App.Mappers;
using Domain.Mappers;
using Domain.Media;
using Domain.Models;
using Domain.Publisher;
using Domain.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WebClient.Models;

namespace App
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Startup Constructor
        /// </summary>
        /// <param name="configuration">Interface of Configuration</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration { get; }


        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container. 
        /// </summary>
        /// <param name="services">services to add to the container</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IPublisher, Publisher>();
            services.AddScoped<INewspaper, Newspaper>();
            services.AddScoped<IRadioStation, RadioStation>();
            services.AddScoped<ISocialMedia, SocialMedia>();

            services.AddScoped<IWeatherForecastService, WeatherForecastService>();
            services.AddScoped<IMapper<WeatherForecastRequestModel, WeatherForecastModel>, WeatherForecastRequestToWeatherForecastMapper>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "DesignPatternSubPub", Version = "v1",
                    Description = "This is a pub/sub example.",
                });
            });
        }


        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">application builder</param>
        /// <param name="env">web host environment</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DesignPattern_SubPub v1"));
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

