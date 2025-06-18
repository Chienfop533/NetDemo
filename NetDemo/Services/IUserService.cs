namespace NetDemo.Services
{
    public interface IUserService
    {
        (string PasswordHash, string Salt) CreatePasswordHashWithSalt(string password);
    }
}
