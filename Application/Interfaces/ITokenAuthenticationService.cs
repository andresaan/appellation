namespace Application.Interfaces
{
    public interface ITokenAuthenticationService
    {
        public Task<string> UseRefreshTokenAsync(string refreshToken);

    }
}
