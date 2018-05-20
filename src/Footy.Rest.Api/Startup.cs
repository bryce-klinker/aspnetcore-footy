using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Footy.Rest.Api.General;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Footy.Rest.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<FootyContext>(o => o.UseNpgsql(_configuration.GetConnectionString("Database")));
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            using (var scope = app.ApplicationServices.CreateScope())
            using (var context = scope.ServiceProvider.GetRequiredService<FootyContext>())
                context.Database.Migrate();

            app.UseMvc();
        }
    }
}