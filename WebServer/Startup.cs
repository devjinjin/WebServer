using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IO;
using System.Linq;
using System.Text;
using WebServer.Data;
using WebServer.Data.Notes;
using WebServer.Data.Place;
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

            //AddTransient: 호출될 때마다 새로운 인스턴스가 생성됨
            //AddScoped : 세션단위로 동일한 인스턴스가 제공됨. 즉, 같은 세션에서는 항상 동일한 객체가 제공되나, 다른 세션이 생성되었다면 그 세션내에서는 그 세션 전용의 객체가 생성되어 제공됨
            //AddSingleton: 세션과는 무관하게 애플리케이션 전체에서 하나의 객체만 생성되어 제공됨

            //Auth
            services.AddAuthentication(options => { /* Authentication options */ })
           .AddGitHub(options =>
           {
               options.ClientId = "49e302895d8b09ea5656";
               options.ClientSecret = "98f1bf028608901e9df91d64ee61536fe562064b";
           });

            //Auth
            services.AddTransient<INoteRepository, NoteRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IPlaceInfoRepository, PlaceInfoRepository>();
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
            //SignalR
            app.UseResponseCompression();
            //SignalR
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
                            Path.Combine(Directory.GetCurrentDirectory(), @"Files")), //실제 폴더 경로
                RequestPath = new PathString("/Temp") // 외부 사용자 경로
            });

            //File 폴더를 위한 경로 설정


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();
        
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages(); // 블레이저 클라이언트를 위한 내용 추가
                endpoints.MapControllers(); // 기존 Api 설정사항
                endpoints.MapFallbackToFile("index.html"); // 블레이저 클라이언트를 위한 내용 추가
            });
        }
    }
}
