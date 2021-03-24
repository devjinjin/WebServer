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
            //DbContext 종속성 주입

            services.AddDbContext<ApplicationDbContext>(options =>
                  options.UseSqlServer(
                      Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);

            JWTAuthentication(services); //jWT 인증 등록 함수

            services.AddTransient<INoteRepository, NoteRepository>();
              services.AddScoped<IProductRepository, ProductRepository>();

            //swagger 등록 
            services.AddSwaggerGen();//Swagger 추가
            //swagger 등록

            //블레이저 클라이언트를 위한 내용 추가
            services.AddServerSideBlazor();
            services.AddRazorPages();
            //블레이저 클라이언트를 위한 내용 추가

            services.AddControllers();
        }

        //jWT 인증 등록 함수
        private void JWTAuthentication(IServiceCollection services)
        {
            //JWT 인증 사용
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
                    ValidateAudience = false, //토큰 유효성 검사 중에 대상의 유효성을 검사할지 여부
                    ValidateIssuer = false, //토큰 유효성 검사 중에 발급자가 유효성을 검사할지 여부
                    ValidateLifetime = false, //토큰 유효성 검사 중에 수명의 유효성을 검사할지 여부
                    RequireExpirationTime = false, //토큰에 만료시간 속성이 필요한지 적용
                    ClockSkew = TimeSpan.Zero, //시간의 유효성을 검사 할 때 적용 
                    ValidateIssuerSigningKey = true //securityToken 에 서명 한 SecurityKey의 유효성 검사 가 호출 되는지 여부
                };
            });
            services.AddScoped<ITokenBuilder, TokenBuilder>(); // 토큰 생성
            //JWT 인증 사용

         
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

            //File 폴더를 위한 경로 설정
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                            Path.Combine(Directory.GetCurrentDirectory(), @"Files")),
                RequestPath = new PathString("/Files")
            });
            //File 폴더를 위한 경로 설정

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
