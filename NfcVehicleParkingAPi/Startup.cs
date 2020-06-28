using System;
using System.Text;
using NfcVehicleParkingAPi.Auth;
using NfcVehicleParkingAPi.Data;
using NfcVehicleParkingAPi.Helpers;
using NfcVehicleParkingAPi.Models;
using NfcVehicleParkingAPi.Services;
using NfcVehicleParkingAPi.ViewModels.Mappings;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;

namespace NfcVehicleParkingAPi
{
    public class Startup
    {
        private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // todo: get this from somewhere secure
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //services.AddCors();
            services.AddCors(options => options.AddPolicy("Cors", builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));

            services.AddDbContext<AuthDbContext>(options =>
                    options.UseSqlServer(
                        Configuration.GetConnectionString("AuthConnectionString")));

            services.AddIdentity<AppUser, AppRole>(
               Configuration =>
               {
                   Configuration.SignIn.RequireConfirmedEmail = false;
                   Configuration.SignIn.RequireConfirmedPhoneNumber = false;
               })
                .AddRoles<AppRole>()
               .AddEntityFrameworkStores<AuthDbContext>();
            services.AddIdentityServer();
            services.AddScoped<IJwtFactory, JwtFactory>();

            //services.Configure<FacebookAuthSettings>(Configuration.GetSection(nameof(FacebookAuthSettings)));
            services.TryAddTransient<IHttpContextAccessor, HttpContextAccessor>();

            // jwt wire up
            // Get options from app settings
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);

            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };



            //Email And Sms Configuration
            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.Configure<SMSoptions>(Configuration);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
                //configureOptions.TokenValidationParameters = new TokenValidationParameters
                //{
                //    NameClaimType = OpenIdConnectConstants.Claims.Subject,
                //    RoleClaimType = OpenIdConnectConstants.Claims.Role
                //};
            });

            //services.Configure<IdentityOptions>(options =>
            //{
            //    options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
            //    options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
            //    options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
            //});
            //// api user claim policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiUser", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.ApiAccess));
                //options.AddPolicy("OnlyAdminAccess", policy => policy.RequireRole("Admin"));
                //options.AddPolicy("Admin", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
                //    options.AddPolicy("RequireRole",
                //policy => policy.RequireRole("Admin", "Handler", "Customer"));
            });

            // add identity
            var builders = services.AddIdentityCore<AppUser>(o =>
            {
                // configure identity options
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
            });
            builders = new IdentityBuilder(builders.UserType, typeof(IdentityRole), builders.Services);
            builders.AddEntityFrameworkStores<AuthDbContext>().AddDefaultTokenProviders();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ViewModelToEntityMappingProfile());
            });
            //services.AddAuthorization();
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            //services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());
            services.AddCors();
            services.AddMvc();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //   app.UseExceptionHandler(
            //builder =>
            //{
            //    builder.Run(
            //               async context =>
            //            {
            //                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            //                var error = context.Features.Get<IExceptionHandlerFeature>();
            //                if (error != null)
            //                {
            //                    context.Response.AddApplicationError(error.Error.Message);
            //                    await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
            //                }
            //            });
            //});

            app.UseAuthentication();

            //app.UseHttpsRedirection();
            app.UseCors("Cors");
            
            //app.UseCors(option => option.WithOrigins("http://localhost:4200/").AllowAnyMethod().AllowAnyHeader());
            app.UseMvc(routes =>
            {
                routes.MapRoute("areaRoute", "{area:exists}/api/{controller}/{action}/{id?}");
            });
        }
    }
}
