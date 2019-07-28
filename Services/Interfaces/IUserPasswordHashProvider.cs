namespace Services.Interfaces
{
    public interface IUserPasswordHashProvider
    {

        /// <summary>
        /// Returns user password hash.
        /// </summary>
        string Hash(string password);
    }
}