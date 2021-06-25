using Hogwarts.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace Hogwarts.Data.Test
{
    public abstract class BaseTest
    {
        public BaseTest()
        {

        }

        

    }

    public class DbTeste : IDisposable
    {
        private string dataBaseName = $"dbhogwarts_test_{Guid.NewGuid().ToString().Replace("-", string.Empty)}";
        public ServiceProvider ServiceProvider { get; private set; }

        public DbTeste()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<MyContext>(o =>
                o.UseSqlServer($"Server=.;Initial Catalog={dataBaseName};MultipleActiveResultSets=true;User ID=UserPotter;Password=Teste@159"), ServiceLifetime.Transient);


            ServiceProvider = serviceCollection.BuildServiceProvider();
            using (var context = ServiceProvider.GetService<MyContext>())
            {
                context.Database.EnsureCreated();
            }
        }

        public void Dispose()
        {
            using (var context = ServiceProvider.GetService<MyContext>())
            {
                context.Database.EnsureDeleted();
            }
        }

    }
}
