using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServer
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
            //블레이저 클라이언트를 위한 내용 추가
            services.AddServerSideBlazor();
            services.AddRazorPages();
            //블레이저 클라이언트를 위한 내용 추가

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //블레이저 클라이언트를 위한 내용 추가
            app.UseBlazorFrameworkFiles(); 
            app.UseStaticFiles();
            //블레이저 클라이언트를 위한 내용 추가

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages(); // 블레이저 클라이언트를 위한 내용 추가
                endpoints.MapControllers(); // 기존 Api 설정사항
                endpoints.MapFallbackToFile("index.html"); // 블레이저 클라이언트를 위한 내용 추가
            });
        }
    }
}
