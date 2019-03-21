using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using PROJ.DataAccess.SessionBuilder;
using PROJ.Logic.Identity;
using PROJ.Logic.Identity.Managers;
using PROJ.Logic.Interfaces;
using PROJ.Logic.Mapping;
using PROJ.Logic.UnitOfWork;
using PROJ.Logic.UnitOfWork.Interfaces;
using PROJ.Logic.UnitOfWork.Managers;
using PROJ.Logic.UnitOfWork.Managers.Interfaces;
using PROJ.Logic.UnitOfWork.Repositories;
using System;
using System.Linq;

namespace PROJ.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSingleton(SessionFactory.BuildConfiguration(Configuration.GetConnectionString("proj"))
                .BuildSessionFactory());

            services.AddScoped(x => x.GetServices<ISessionFactory>().First().OpenSession());

            services.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequiredLength = 6;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
            });

            services.AddIdentityCore<AppIdentityUser>()
                .AddDefaultTokenProviders()
                .AddUserStore<AppIdentityStore>()
                .AddUserManager<IdentityUserManager>()
                .AddSignInManager<SignInManager>()
                .AddErrorDescriber<AppIdentityErrorDescriber>();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                opt.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
                opt.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
            }).AddIdentityCookies();

            services.ConfigureApplicationCookie(opt =>
            {
                opt.Cookie.HttpOnly = true;
                opt.ExpireTimeSpan = TimeSpan.FromDays(365);
                opt.LoginPath = "/Identity/Account/Login";
                opt.LogoutPath = "/User/Logout";
                opt.AccessDeniedPath = "/Identity/Account/AccessDenied";
            });

            services.AddDistributedMemoryCache();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAppContext, Infrastructure.AppContext>();

            services.AddScoped<AppIdentityStore>();
            services.AddScoped<AppIdentityErrorDescriber>();

            // Lazy services
            services.AddScoped(provider => new Lazy<IdentityUserManager>(() => provider.GetService<IdentityUserManager>()));
            services.AddScoped(provider => new Lazy<ProjectRepository>(() => provider.GetService<ProjectRepository>()));
            services.AddScoped(provider => new Lazy<UserRepository>(() => provider.GetService<UserRepository>()));
            services.AddScoped(provider => new Lazy<TeamRepository>(() => provider.GetService<TeamRepository>()));
            services.AddScoped(provider => new Lazy<TaskRepository>(() => provider.GetService<TaskRepository>()));

            // Repositories
            services.AddScoped<UserRepository>();
            services.AddScoped<UserClaimRepository>();
            services.AddScoped<ProjectRepository>();
            services.AddScoped<TaskGroupRepository>();
            services.AddScoped<TaskRepository>();
            services.AddScoped<ChecklistRepository>();
            services.AddScoped<ChecklistTaskRepository>();
            services.AddScoped<CommentRepository>();
            services.AddScoped<TeamRepository>();

            // UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Managers
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IUserClaimManager, UserClaimManager>();
            services.AddScoped<IProjectManager, ProjectManager>();
            services.AddScoped<ITaskGroupManager, TaskGroupManager>();
            services.AddScoped<ITaskManager, TaskManager>();
            services.AddScoped<IChecklistManager, ChecklistManager>();
            services.AddScoped<IChecklistTaskManager, ChecklistTaskManager>();
            services.AddScoped<ICommentManager, CommentManager>();
            services.AddScoped<ITeamManager, TeamManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Dashboard}/{id?}");
            });

            InitializeAutoMapper();
        }

        private static void InitializeAutoMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<AutoMapperLogicProfile>();
            });
        }
    }
}
