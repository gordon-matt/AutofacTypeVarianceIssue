using Autofac;
using AutofacTypeVarianceIssue.Data;
using Extenso.Data.Entity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AutofacTypeVarianceIssue
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                })
                .UseServiceProviderFactory(new CustomAutofacServiceProviderFactory(builder => ConfigureContainer(builder)));

        public static void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDbContextFactory>().As<IDbContextFactory>();
            builder.RegisterGeneric(typeof(TestRepository<,>)).As(typeof(ITestRepository<,>)).SingleInstance();
        }
    }
}