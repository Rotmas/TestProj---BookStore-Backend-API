using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Tests;

internal class ApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
    where TStartup : class
{
    readonly Func<IServiceCollection, IServiceCollection> Register;

    public ApplicationFactory(Func<IServiceCollection, IServiceCollection> register)
        => Register = register;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
        => builder.ConfigureServices(services => Register(services));
}