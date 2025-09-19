using Microsoft.AspNetCore.Components.Authorization;

namespace NebulaOps.Utils;
public interface ICustomAuthenticationStateProvider
{
    Task<AuthenticationState> GetAuthenticationStateAsync();

    Task<string> GetToken();

    Task<string> GetUser();

    void Initialize();

    Task MarkUserAsAuthenticated(string token);

    Task MarkUserAsLoggedOut();

    Task SetUserToken(string user = "", string token = "");
}
