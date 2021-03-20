using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebServer.Data;

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
            //DbContext 종속성 주입

            services.AddDbContext<ApplicationDbContext>(options =>
                  options.UseSqlServer(
                      Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);


            //swagger 등록 
            services.AddSwaggerGen();//Swagger 추가
            //swagger 등록

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

            //swagger 등록 
            app.UseSwagger(); //swagger 등록                            
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            //swagger 등록

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
