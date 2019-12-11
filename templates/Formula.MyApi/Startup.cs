using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Formula.SimpleRepo;
using Formula.SimpleAuthServer;
using Formula.SimpleAuthServerUI;
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

        protected IServiceCollection SetAuthorizationDefaults(IServiceCollection services)
        {
            // As a last step allow "authorization" on the following schemes.
            // (Cookies or JWT)
            // Any controller tagged with [Authorize], can be accessed by the 
            // Schemes in the following list.
            // Otherwise be explicit on your routes by specifying the 
            // scheme or policy.
            // 
            // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
            // [Authorize(Policy = "TrialUser")]
            // 
            // If we are running Auth Server and Resource Server and 
            // https://docs.microsoft.com/en-us/aspnet/core/security/authorization/limitingidentitybyscheme?view=aspnetcore-3.1
            services.AddAuthorization( options => {
                options.DefaultPolicy = (new AuthorizationPolicyBuilder(
                    // Used for simple OAuth2 / JWT Bearer schemes (Simple Auth Server)
                    JwtBearerDefaults.AuthenticationScheme,

                    // The following two are used when adding Auth Server UI to the project
                    // And want to allow OpenID Connect Challenges over Cookies
                    CookieAuthenticationDefaults.AuthenticationScheme
                )).RequireAuthenticatedUser().Build();
            });

            return services;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

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

            // Uncomment in order to use OAuth2 / OpenID Connect server
            //services.AddSimpleAuthServer(this.Configuration, migrationsAssembly);
            //services.AddSimpleAuthServerUI();

            // Uncomment in order to use resource server
            //services.AddSimpleResourceServer(this.Configuration);

            this.SetAuthorizationDefaults(services);
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
            //app.UseSimpleAuthServer(this.Configuration);
            //app.UseSimpleAuthServerUI();

            // Uncomment in order to use resource server
            //app.UseSimpleResourceServer();

            // Adds the authorization middleware to make sure, our API endpoint cannot be accessed by anonymous clients.
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
