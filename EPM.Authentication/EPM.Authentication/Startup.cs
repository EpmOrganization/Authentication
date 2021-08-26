using EPM.Authentication.Context;
using EPM.Authentication.Model.ConfigModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPM.Authentication
{
    public class Startup
    {
        // ȫ�ֱ�������������ֿ�������
        readonly string CustomerSpecificOrigins = "CustomerSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region �������ݿ�����
            // ��ȡ������Ϣ
            ConnectionStrings connectionStrings = new ConnectionStrings();
            Configuration.GetSection("ConnectionString").Bind(connectionStrings);
            var serverVersion = new MySqlServerVersion(new Version(connectionStrings.Major, connectionStrings.Minor, connectionStrings.Build));
            //�������������
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySql(connectionString: connectionStrings.DbConnection, serverVersion: serverVersion);
            });
            #endregion

            #region �����֤
            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));
            JwtConfig jwtConfig = Configuration.GetSection("JwtConfig").Get<JwtConfig>();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddScheme<JwtBearerOptions, AuthenticationHandler<JwtBearerOptions>>(JwtBearerDefaults.AuthenticationScheme, x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig.IssuerSigningKey)),
                    ValidIssuer = jwtConfig.Issuer,
                    ValidAudience = jwtConfig.Audience,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    RequireExpirationTime = true
                };
            });

            #endregion

            #region ��ӿ���
            // ��ӿ������
            services.AddCors(options =>
            {
                //���ÿ���������������Դ
                options.AddPolicy(CustomerSpecificOrigins,
                 builder => builder.AllowAnyOrigin()
                 .AllowAnyHeader()
                 .WithExposedHeaders("Content-Disposition")
                 .AllowAnyMethod());
            });
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

            #region ���������֤
            app.UseAuthentication();
            #endregion

            app.UseRouting();
            #region ��ӿ����м�� ��Ҫ������UseRouting��UseAuthorization֮��

            app.UseCors(CustomerSpecificOrigins);
            #endregion

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //���������RequireCors������CustomerSpecificOrigins����ConfigureServices���������õĿ����������
                endpoints.MapControllers().RequireCors(CustomerSpecificOrigins);
            });
        }
    }
}
