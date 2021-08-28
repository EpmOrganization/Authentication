using EPM.Authentication.Data.Context;
using EPM.Authentication.Data.Uow;
using EPM.Authentication.Model.ConfigModel;
using EPM.Authentication.Repository;
using EPM.Authentication.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

namespace EPM.Authentication
{
    public class Startup
    {
        // 全局变量，后面的名字可以随意
        readonly string CustomerSpecificOrigins = "CustomerSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Appsettings.Instance().Initial(Configuration);

            #region 配置数据库连接
            // 获取配置信息
            ConnectionStrings connectionStrings = new ConnectionStrings();
            Configuration.GetSection("ConnectionString").Bind(connectionStrings);
            var serverVersion = new MySqlServerVersion(new Version(connectionStrings.Major, connectionStrings.Minor, connectionStrings.Build));
            //添加数据上下文
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySql(connectionString: connectionStrings.DbConnection, serverVersion: serverVersion);
            });
            #endregion

            #region 身份验证
            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));
            JwtConfig jwtConfig = Configuration.GetSection("JwtConfig").Get<JwtConfig>();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            // 使用JWT验证
            .AddJwtBearer();

            #endregion

            #region 添加跨域
            // 添加跨域服务
            services.AddCors(options =>
            {
                //配置跨域处理，允许所有来源
                options.AddPolicy(CustomerSpecificOrigins,
                 builder => builder.AllowAnyOrigin()
                 .AllowAnyHeader()
                 .WithExposedHeaders("Content-Disposition")
                 .AllowAnyMethod());
            });
            #endregion

            services.Configure<LoginLockConfig>(Configuration.GetSection("LoginLockConfig"));

            #region 注入
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ITokenInfoRepository, TokenInfoRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork<AppDbContext>>();
            #endregion

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EPM.Authentication", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EPM.Authentication v1"));
            }

            app.UseHttpsRedirection();

            #region 启用身份认证
            app.UseAuthentication();
            #endregion

            app.UseRouting();
            #region 添加跨域中间件 需要设置再UseRouting和UseAuthorization之间

            app.UseCors(CustomerSpecificOrigins);
            #endregion

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //跨域需添加RequireCors方法，CustomerSpecificOrigins是再ConfigureServices方法中配置的跨域策略名称
                endpoints.MapControllers().RequireCors(CustomerSpecificOrigins);
            });
        }
    }
}
