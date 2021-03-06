using AutoMapper;
using JobOffersMVC.Filters;
using JobOffersMVC.Repositories;
using JobOffersMVC.Repositories.Abstractions;
using JobOffersMVC.Repositories.Implementations;
using JobOffersMVC.Services.AutoMapper;
using JobOffersMVC.Services.Helpers;
using JobOffersMVC.Services.ModelServices.Abstractions;
using JobOffersMVC.Services.ModelServices.Implementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace JobOffersMVC
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
            services.AddHttpContextAccessor();
            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddSingleton(Configuration.GetSection("AppSettings").Get<AppSettings>());

            services.AddAutoMapper(m => m.AddProfile(new AutoMapperConfiguration()));

            services.AddDbContext<JobOffersDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IJobOffersRepository, JobOffersRepository>();
            services.AddScoped<IUserApplicationsRepository, UserApplicationsRepository>();
            services.AddScoped<ICommentsRepository, CommentsRepository>();

            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IJobOffersService, JobOffersService>();
            services.AddScoped<IUserApplicationsService, UserApplicationsService>();
            services.AddScoped<ICommentsService, CommentsService>();

            services.AddScoped<IFileHelperService, FileHelperService>();
            services.AddScoped<IEmailService, EmailService>();

            services.AddScoped<AuthenticationFilter>();
            services.AddScoped<AuthorizationFilter>();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, JobOffersDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            context.Database.Migrate();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
