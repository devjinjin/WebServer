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
            //DbContext ���Ӽ� ����

            services.AddDbContext<ApplicationDbContext>(options =>
                  options.UseSqlServer(
                      Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);


            //swagger ��� 
            services.AddSwaggerGen();//Swagger �߰�
            //swagger ���

            //������ Ŭ���̾�Ʈ�� ���� ���� �߰�
            services.AddServerSideBlazor();
            services.AddRazorPages();
            //������ Ŭ���̾�Ʈ�� ���� ���� �߰�

            services.AddControllers();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //swagger ��� 
            app.UseSwagger(); //swagger ���                            
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            //swagger ���

            //������ Ŭ���̾�Ʈ�� ���� ���� �߰�
            app.UseBlazorFrameworkFiles(); 
            app.UseStaticFiles();
            //������ Ŭ���̾�Ʈ�� ���� ���� �߰�

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

        
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages(); // ������ Ŭ���̾�Ʈ�� ���� ���� �߰�
                endpoints.MapControllers(); // ���� Api ��������
                endpoints.MapFallbackToFile("index.html"); // ������ Ŭ���̾�Ʈ�� ���� ���� �߰�
            });
        }
    }
}
