using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace Hogwarts.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            //Terminal -- dotnet ef migrations add Hogwarts_Migrations
            //dotnet ef database update
            //Usado para Criar as Migrações
            var connectionString = "Server=.;Initial Catalog=dbhogwarts;MultipleActiveResultSets=true;User ID=UserPotter;Password=Teste@159";
            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new MyContext(optionsBuilder.Options);
        }
    }
}
