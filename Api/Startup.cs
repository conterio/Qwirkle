using Autofac;
using Busi;
using Busi.Helpers;
using Busi.IBusi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using System;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IContainer ApplicationContainer { get; private set; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add services to the collection.
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<PlayerRepository>();
			services.AddSingleton<IShuffleHelper, ShuffleHelper>();
            services.AddTransient<IGameBusi, GameBusi>();

            services.AddSignalR(options => options.EnableDetailedErrors = true);
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", x =>
                {
                    x.AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithOrigins("http://172.24.49.138:49796")
                        .AllowCredentials();
                });
            });

            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors("CorsPolicy");
            app.UseSignalR(routes => routes.MapHub<ServerManagerHub>("/hub"));
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}