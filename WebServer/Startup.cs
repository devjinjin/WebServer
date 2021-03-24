using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IO;
using System.Text;
using WebServer.Data;
using WebServer.Data.Notes;
using WebServer.Data.Products;
using WebServer.Data.Users;

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

            JWTAuthentication(services); //jWT ���� ��� �Լ�

            services.AddTransient<INoteRepository, NoteRepository>();
              services.AddScoped<IProductRepository, ProductRepository>();

            //swagger ��� 
            services.AddSwaggerGen();//Swagger �߰�
            //swagger ���

            //������ Ŭ���̾�Ʈ�� ���� ���� �߰�
            services.AddServerSideBlazor();
            services.AddRazorPages();
            //������ Ŭ���̾�Ʈ�� ���� ���� �߰�

            services.AddControllers();
        }

        //jWT ���� ��� �Լ�
        private void JWTAuthentication(IServiceCollection services)
        {
            //JWT ���� ���
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = true;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("placeholder-key-that-is-long-enough-for-sha256")),
                    ValidateAudience = false, //��ū ��ȿ�� �˻� �߿� ����� ��ȿ���� �˻����� ����
                    ValidateIssuer = false, //��ū ��ȿ�� �˻� �߿� �߱��ڰ� ��ȿ���� �˻����� ����
                    ValidateLifetime = false, //��ū ��ȿ�� �˻� �߿� ������ ��ȿ���� �˻����� ����
                    RequireExpirationTime = false, //��ū�� ����ð� �Ӽ��� �ʿ����� ����
                    ClockSkew = TimeSpan.Zero, //�ð��� ��ȿ���� �˻� �� �� ���� 
                    ValidateIssuerSigningKey = true //securityToken �� ���� �� SecurityKey�� ��ȿ�� �˻� �� ȣ�� �Ǵ��� ����
                };
            });
            services.AddScoped<ITokenBuilder, TokenBuilder>(); // ��ū ����
            //JWT ���� ���

         
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

            //File ������ ���� ��� ����
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                            Path.Combine(Directory.GetCurrentDirectory(), @"Files")),
                RequestPath = new PathString("/Files")
            });
            //File ������ ���� ��� ����

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
