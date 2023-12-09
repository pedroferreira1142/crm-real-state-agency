using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using projetoCurricular_v002.Schedulers;
using Quartz.Spi;

namespace projetoCurricular_v002
{
    public class Startup
    {
        
        public IHostEnvironment ihe { get; set; }
        public IConfiguration ic { get; set; }

        public Startup(IConfiguration conf, IHostEnvironment env)
        {
            this.ihe = env;
            this.ic = conf;
            Program._ligacao = this.ic.GetValue<string>("ConnectionStrings:DefaultConnection");
        }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddMvc();

            /*------------------- Serviços Quartz -------------------------*/
            services.AddHostedService<QuartzHostedService>();
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            /*-------------------------------------------------------------*/

            /*------------------- Quartz Jobs -------------------*/
            //Criar Lembrete
            services.AddSingleton<LembreteJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(LembreteJob),
                cronExpression: "0 0/1 * 1/1 * ? *"));
            /*---------------------------------------------------*/
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                /*------------------- Controlador Padrão ---------------------*/
                endpoints.MapControllerRoute(name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                /*------------------------------------------------------------*/

                /*------------------- Controlador Cliente --------------------*/
                endpoints.MapControllerRoute(name: "cliente",
                    pattern: "{controller=Cliente}/{action=Listar}/{id?}");

                endpoints.MapControllerRoute(name: "cliente",
                    pattern: "{controller=Cliente}/{action=Cliente}/{id?}");
                /*------------------------------------------------------------*/

                /*------------------- Controlador Transação -------------------*/
                endpoints.MapControllerRoute(name: "transacao",
                    pattern: "{controller=Transacao}/{action=Criar}/{id?}");

                /*-------------------------------------------------------------*/

            });

        }
    }
}
