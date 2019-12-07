using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Formula.SimpleRepo;
using Formula.SimpleMembership;
using Formula.SimpleAuthServer;
using Formula.SimpleResourceServer;

namespace Formula.MyApi
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
            services.AddCors( options =>
            {
                options.AddPolicy( "AllowCors", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()                        
                        .AllowAnyMethod();
                });
            });

            services.AddControllers();
            services.AddRepositories();

            // Uncomment in order to use simple cookie based local storage authentication
            //services.AddSimpleMembership(this.Configuration, typeof(Startup).Assembly.GetName().Name);

            // Uncomment in order to use OAuth2 / OpenID Connect server
            //services.AddSimpleAuthServer(this.Configuration);

            // Uncomment in order to use resource server
            //services.AddSimpleResourceServer(this.Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllowCors");

            // Uncomment in order to use OAuth2 / OpenID Connect server
            //app.UseSimpleAuthServer();

            // Uncomment in order to use resource server
            //app.UseSimpleResourceServer();

            // Adds the authorization middleware to make sure, our API endpoint cannot be accessed by anonymous clients.
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
