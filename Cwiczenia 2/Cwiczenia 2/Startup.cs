using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cwiczenia2.Logic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cwiczenia_2
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
            //tutaj dodajemy interefejsy
            //addTRansient
            services.AddSingleton<StudentDb, StudentDbImpl>();
            services.AddSingleton<IEnrolmentDb, IEnrolementDbImpl>();
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, StudentDb studentDb)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //app.UseMiddleware<LoggingMiddleware>(); -> tutaj użyć tego logowania

            //tutaj dodajemy middle weary context - httpcontext, next to funkcja ktory wskazuje kolejny middle wear
            //w hederze trzeba dodac Index
            app.Use(async (context, next) =>
            {
                //if(!context.Request.Headers.ContainsKey("Index"))
               // {
              //      context.Response.StatusCode = StatusCodes.Status401Unauthorized;
               //     await context.Response.WriteAsync("nie podano w nagłówku indexu");
             //       return;
             //   }
            //    var index = context.Request.Headers["Index"].ToString();

                //Tak odczytujemy body z zadania 6.2
                //var bodyStream = string.Empty;
                //using (var reader = StreamReader(HttpContext.Request.Body, Encoding.UTF8, true, 1024, true))
                //{
                //    bodyStream = async reader.ReadToEndAsync();
                //}

                //Task invoke Async trzeba przestawic na poczatek w streamie
                //request enable buffering
                //raz tworzyc plik i dopisywac do pliku 6.2 umiescic na gorze zeby dorzycic na samym poczatku


                    //połaczenie z baza danych i sprawdznie czy student o takim indexie istnieje
                    // pierwsze zadania z zestawu 6. dopisac metode w kontrolerze 
                    //if (!studentDb.getStudentsFromDb(index))  //ma sprawdzic czy student o takim indexie istnieje w bazie danych 
                    // {                                           //mozna zrobic przez zwyklego selekta i tyle
                    //       context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    //          await context.Response.WriteAsync("nie podano w nagłówku indexu");
                    //         return;
                    //     }

                    //midle wear z zapisanem do pliku to jest zadanie 2


                    await next(); // przetwarzanie idzie dalej

            });

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
