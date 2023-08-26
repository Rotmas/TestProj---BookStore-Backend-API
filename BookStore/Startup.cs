using Data.EF;
using Logic;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Model;

namespace BookStore;

public class Startup
{
    public void ConfigureServices(IServiceCollection services) => services
        .AddDbContext<BookStoreDbContext>(options => options.UseSqlServer("Server=localhost;Database=master;Trusted_Connection=True;Encrypt=False;"), ServiceLifetime.Singleton)
        .AddSingleton<IBookRepository, EFBookRepository>()
        .AddSingleton<IOrderRepository, EFOrderRepository>()
        .AddSingleton<Books>()
        .AddSingleton<Orders>()
        .AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "BookStore API", Version = "v1" });
            options.EnableAnnotations();
        })
        .AddControllers();

    public void Configure(IApplicationBuilder app, IWebHostEnvironment _) => app
        .UseMiddleware<NotFoundExceptionMiddleware>()
        .UseRouting()
        .UseEndpoints(endpoints => endpoints.MapControllers());
}
