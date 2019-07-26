using System;
using Microsoft.Extensions.Configuration;
using Models.Repository;
using Xunit;

namespace ModelTests
{
    public class Models
    {
        private readonly IConfiguration iConfiguration;

        [Fact]
        public async System.Threading.Tasks.Task CanQueryDatabaseWithRepositoryAndDapperAsync()
        {
            SensorsRepository sensorsRepo = new SensorsRepository(iConfiguration);
            var result = await sensorsRepo.GetById(1);
        }
    }
}
