using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using JointTelegramBot.Web.Configuration;
using JointTelegramBot.Web.Data;
using JointTelegramBot.Web.Services;
using JointTelegramBot.Web.Services.Data;
using JointTelegramBot.Web.Services.Telegram;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace JointTelegramBot.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer Container { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterInstance(Configuration).As<IConfiguration>().SingleInstance();

            var serviceCollection = new ServiceCollection().AddLogging(loggingBuilder =>
                loggingBuilder.AddSerilog(dispose: true));

            serviceCollection.BuildServiceProvider();

            containerBuilder.Populate(serviceCollection);

            serviceCollection.AddDbContextPool<JointBotContext>(options => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TelegramBot;Trusted_Connection=True;MultipleActiveResultSets=true"));
            //services.AddDbContext<JointBotContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            containerBuilder.RegisterType<TelegramConfig>().SingleInstance();
            containerBuilder.RegisterType<TelegramMessageReceiveService>().SingleInstance();
            containerBuilder.RegisterType<DatabaseService>().AsSelf();
            containerBuilder.RegisterType<TelegramBot>().SingleInstance();
            containerBuilder.RegisterType<StartupCheckingService>().SingleInstance();
            Container = containerBuilder.Build();

            var loggerFactory = Container.Resolve<ILoggerFactory>();
            var log = loggerFactory.CreateLogger<Program>();
            ConfigureConfig(Container, Configuration,log);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime applicationLifetime)
        {
            applicationLifetime.ApplicationStarted.Register(async () => await OnStarting());

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private static void ConfigureConfig(IContainer container, IConfiguration configuration,ILogger<Program> log)
        {
            try
            {
                var config = container.Resolve<TelegramConfig>();
                configuration.GetSection("Telegram").Bind(config);
                log.LogInformation("Created config");
                
            }
            catch (Exception)
            {

                log.LogError("Error in reading telegram config");
                throw;
            }
        }

        private async Task OnStarting()
        {
            var startupService = Container.Resolve<StartupCheckingService>();
            var context = Container.Resolve<JointBotContext>();
            await DbInitializer.Initialize(context);

            startupService.Start();
        }
    }
}
