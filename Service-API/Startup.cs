using Middleware.Shared.Startups;
using Middleware.Intercom.Protos;
using Middleware.Intercom.Setting;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Service_API
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
            ConfigureGeneralServices(services);

            #region Mongo

            #endregion Mongo

            #region SQL

            #endregion SQL

            #region Redis

            #endregion Redis

            #region Services

            services.AddHttpContextAccessor();

            //services.AddScoped<ITimeZoneService, TimeZoneService>();

            #endregion Services

            #region Grcp Services

            var grcpServiceSetting = new GrcpServiceSetting(Configuration);

            services.AddGrpcClient<ConfigProto.ConfigProtoClient>(o =>
            {
                o.Address = new Uri(grcpServiceSetting.GetSetting("Config").Url);
            });

            #endregion Grcp Services
        }

        public void ConfigureGeneralServices(IServiceCollection services)
        {
            #region controllers

            services.AddControllers();


            #endregion controllers

            #region log

            #endregion log

            #region ActionContextAccessor

            services.AddScoped<IActionContextAccessor, ActionContextAccessor>();

            #endregion ActionContextAccessor

            #region AutoMapper


            #endregion AutoMapper

            #region HttpContextAccessor

            services.AddHttpContextAccessor();

            #endregion HttpContextAccessor

            #region Swagger
            StartupHelper.CreateSwagger(services);

            #endregion Swagger

            #region grpc

            services.AddGrpc();

            #endregion grpc
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //grpc
                //endpoints.MapGrpcService<TemplateService>();
            });
        }
    }
}

