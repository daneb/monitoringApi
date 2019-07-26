using System;
using System.IO;
using Models.Repository;
using Xunit;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration;

namespace Integration
{
    public class Models
    {

        [Fact]
        public async System.Threading.Tasks.Task CanQueryDatabaseWithRepositoryAndDapperAsync()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            SensorsRepository sensorsRepo = new SensorsRepository(configuration);
            var result = await sensorsRepo.GetById(1);
        }
    }
}
