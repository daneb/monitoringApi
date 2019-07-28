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
            UserPasswordHashProvider userPasswordHashProvider = new UserPasswordHashProvider();
            var result = userPasswordHashProvider.Hash("123");

            Assert.Equal(expected, result);
        }
    }
}