using Microsoft.Extensions.Configuration;
using Models.Repository;
using Services;
using Xunit;

namespace Integration
{
    public class EncryptionTest
    {
        [Fact]
        public void SuccessGeneratingHash()
        {
            string expected = "enc:428TI2eXK7c=";
            IConfiguration config = null;
            AuthenticationService authenticationService = new AuthenticationService(new UsersRepository(config), new UserProjectPermissionsRepository(config));
            var result = authenticationService.Hash("123");

            Assert.Equal(expected, result);
        }
    }
}