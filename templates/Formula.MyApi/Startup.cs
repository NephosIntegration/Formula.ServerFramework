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
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Identity;
using Formula.SimpleRepo;
using Formula.SimpleAuthServer;
using Formula.SimpleAuthServerUI;
using Formula.SimpleResourceServer;
using Formula.SimpleMembership;

namespace Formula.MyApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Will add authentication and authorization to the project
        protected void AddAuth(IServiceCollection services)
        {
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            // Uncomment in order to use OAuth2 / OpenID Connect server
            // If you wish to enable identity services, this must be called before
            // other calls to AddAuthentication
            var identityServerBuilder = services.AddSimpleAuthServer(this.Configuration, migrationsAssembly);

            // If any of your controller endpoints will be guarded by some form of authentication
            // You must wire up the authentication into the dependency injection system
            // Additional services may use this builder to add additional
            // Schemes or services
            var authenticationBuilder = services.AddAuthentication(options => {
                
                // We are using a cookie to locally sign-in the user (via "Cookies" as the DefaultScheme), 
                // and we set the DefaultChallengeScheme to oidc because when we need the user to login, 
                // we will be using the OpenID Connect protocol.

                // Use cookies as the fallback default scheme for all the other defaults.
                // This requires a handler be wired up at some point
                // Such as in the SimpleAuthServerUI's AddSimpleAuthServerUI call below
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                // If a "challenge" via ChallengeAsync is made, handle it using OpenID Connect by default
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            });

            // Uncomment in order to use AspNetIdentity
            //services.AddSimpleMembership(this.Configuration, migrationsAssembly);

            // Uncomment the following line in order to be able to use AspNetIdentity within the authorization server
            //identityServerBuilder.AddAspNetIdentity<ApplicationUser>();

            // Uncomment in order to provide the views necessary to view work with the identity server
            //services.AddSimpleAuthServerUI(authenticationBuilder);

            // Uncomment in order to use resource server
            //services.AddSimpleResourceServer(authenticationBuilder);

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
            // If we are running Auth Server and Resource Server in same location
            // https://docs.microsoft.com/en-us/aspnet/core/security/authorization/limitingidentitybyscheme?view=aspnetcore-3.1
            services.AddAuthorization( options => {
                options.DefaultPolicy = (new AuthorizationPolicyBuilder(
                    // Used for simple OAuth2 / JWT Bearer schemes (Simple Auth Server)
                    JwtBearerDefaults.AuthenticationScheme

                    // Used for for identity server issued cookies
                    , CookieAuthenticationDefaults.AuthenticationScheme

                    // Used for simple memberships and identities
                    , IdentityConstants.ApplicationScheme

                )).RequireAuthenticatedUser().Build();
            });
        }

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

            this.AddAuth(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllowCors");

            // Uncomment in order to use resource server
            //app.UseSimpleResourceServer();

            // Uncomment in order to use OAuth2 / OpenID Connect server
            //app.UseSimpleAuthServer(this.Configuration);

            // Uncomment if you want your auth server to provide views for log in / log out / consent etc..
            //app.UseSimpleAuthServerUI();

            // Adds the authorization middleware to make sure, our API endpoint cannot be accessed by anonymous clients.
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
